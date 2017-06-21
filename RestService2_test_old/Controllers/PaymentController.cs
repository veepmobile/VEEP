using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using HttpUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestService.BLL;
using RestService.Models;
using RestService.CommService;
using Calabonga.Xml.Exports;

namespace RestService.Controllers
{
    public class PaymentController : Controller
    {

        public ActionResult Result(int restaurantID, string orderId)
        {
            string ret = "";
            int mode = 1;
            decimal order_rest_sum;

            if (!String.IsNullOrWhiteSpace(orderId))
            {
                mode = Configs.GetMode(restaurantID);

                //Запрашиваем статус заказа в платежной системе
                orderStatusResponse response = new orderStatusResponse();
                response = MerchantData.getOrderStatus(orderId,mode);
                if (response != null)
                {
                    order_rest_sum = Convert.ToDecimal(response.Amount / 100);
                    //Вставка данных о платеже в БД
                    string result = OrderData.SqlInsertStatus(response.OrderNumber, orderId, response.OrderStatus, response.ErrorCode, response.ErrorMessage, response.Pan, response.expiration, response.cardholderName, response.Amount, response.Ip, response.clientId, response.bindingId, order_rest_sum);

                    Helper.saveToLog(0, "", "getOrderStatus", "restaurantID=" + restaurantID.ToString() + "orderId=" + orderId + ", orderStatus=" + response.OrderStatus.ToString() + ", errorCode=" + response.ErrorCode.ToString() + ", errorMessagge=" + response.ErrorMessage + ", orderNumber=" + response.OrderNumber + ", pan=" + response.Pan + ", expiration=" + response.expiration + ", cardholderName=" + response.cardholderName + ", amount=" + response.Amount.ToString() + ", ip=" + response.Ip + ", clientId=" + response.clientId + ", bindingId=" + response.bindingId, "Результат платежа: errorCode=" + response.ErrorCode.ToString() + ", errorMessage=" + response.ErrorMessage, 0);

                    //Вставка связки в БД
                    string bind = OrderData.SqlInsertBinding(response.clientId, response.bindingId, response.Pan, response.expiration, 1);

                    if (response.OrderStatus == 2)
                    {

                        //Отправка email об оплате
                        SendEmail(restaurantID,orderId);

                        Order order = new Order();
                        order = OrderData.SqlFindOrder(response.OrderNumber); //ищем заказ по order_number

                        int tip = 0;
                        if (order.TippingProcent > 0)
                        {
                            //Оплата чаевых
                            //расчет чаевых в рубл с округлением

                            //decimal sum = (order.TippingProcent * order.OrderPayment.OrderSum) / 100;
                            decimal sum = (order.TippingProcent * order_rest_sum) / 100;

                            decimal tippingSum = Decimal.Round(sum);
                            //double tippingSum = Math.Round(((double)order.TippingProcent * order.OrderPayment.OrderBank) / 100);
                            tip = GetPaymentTipping(order.PhoneCode, order.PhoneNumber, restaurantID, order.TableID, order.OrderNumber, order.UserKey, response.clientId, response.bindingId, (order.OrderPayment.OrderBank/100).ToString(), tippingSum, order.TippingProcent, order.Waiter.ID, order.Waiter.Name);
                            if (tip != 0)
                            {
                                Helper.saveToLog(0, order.UserKey, "GetPaymentTipping", "restaurantID=" + order.RestaurantID.ToString() + ", orderNumber=" + order.OrderNumber + ", tableID=" + order.TableID + ", orderSum=" + (order.OrderPayment.OrderBank / 100).ToString() + ", tippringProcent=" + order.TippingProcent.ToString() + ", tippingSum=" + tippingSum.ToString() + ", waiterID=" + order.Waiter.ID.ToString() + ",waiterName=" + order.Waiter.Name, "Оплата чаевых прошла", 0);
                            }
                            else
                            {
                                Helper.saveToLog(0, order.UserKey, "GetPaymentTipping", "restaurantID=" + order.RestaurantID.ToString() + ", orderNumber=" + order.OrderNumber + ", tableID=" + order.TableID + ", orderSum=" + (order.OrderPayment.OrderBank / 100).ToString() + ", tippringProcent=" + order.TippingProcent.ToString() + ", tippingSum=" + order.TippingSum.ToString() + ", waiterID=" + order.Waiter.ID.ToString() + ",waiterName=" + order.Waiter.Name, "Чаевых нет", 1);
                            }
                        }


                        //Закрытие оплаченного заказа в ИМ
                        string saveStatus = "";
                        if (order != null)
                        {
                            //Запись статуса "заказ оплачен. стол не закрыт" в БД
                            saveStatus = OrderData.UpdateOrderStatus(response.OrderNumber, 2);

                            //Проверка ресторана на возможность онлайн оплаты
                            if(OrderData.SqlCheckRestaurantPayment(order.RestaurantID))
                            {

                            //Запрос к Интеграционному модулю на закрытие стола               
                                string endpointName = "";
                                string address = "";

                                endpointName = Configs.GetEndpoint(restaurantID);
                                address = Configs.GetAddress(restaurantID);


                            IntegrationCMD.IntegrationCMDClient cmd = new IntegrationCMD.IntegrationCMDClient(endpointName, address);
                            IntegrationCMD.Order[] orders = cmd.GetPayment(order.RestaurantID, order.OrderNumber, order.OrderPayment.OrderSum, DateTime.Now);
                            int error = 0;
                            string error_msg = "";
                            foreach (var item in orders)
                            {
                                if (item.ErrorCode == 20) //Ошибка: Заказ оплачен. Сумма не совпадает                             
                                {
                                    error = 20;
                                    error_msg = item.Error;
                                    break;
                                }

                                if (item.ErrorCode != 0) //Любая другая ошибка
                                {
                                    error = item.ErrorCode;
                                    error_msg = item.Error;
                                    break;
                                }
                           //     OrderStatus os = new OrderStatus();
                           //     os.StatusID = item.OrderStatus.StatusID;
                           //     os.StatusDate = item.OrderStatus.StatusDate;
                           //     order.OrderStatus = os;
                            }

                            //Для теста
                            //error = 20;
                            //error = 1;

                            int call = 0;

                            if (error == 0)
                            {
                                //Заказ оплачен. Стол закрыт
                                //Отправка сообщения официанту об успешной оплате заказа
                                call = cmd.CallWaiter(order.RestaurantID, order.TableID, order.OrderNumber, 8);

                                //Запись статуса ""заказ оплачен. стол закрыт"" в БД
                                saveStatus = OrderData.UpdateOrderStatus(response.OrderNumber, 3);

                                //Запись сообщения об оплате заказа                               
                                string tippingProc = (tip > 0) ? "Сервис - " + order.TippingProcent.ToString() + "%":"";
                                Messages msg = new Messages { RestaurantID = order.RestaurantID, TableID = order.TableID, MessageType = 8, MessageText = "Заказ оплачен на сумму " + order.OrderPayment.OrderSum.ToString() + " р." + tippingProc, ErrorCode = error, ErrorText = error_msg };
                                int save_msg = PersonalData.SqlInsertMessage(msg);

                            }
                            else
                            {
                                if (error == 20)
                                {
                                    //Ошибка: Заказ оплачен. Сумма не совпадает 
                                    //Отправка сообщения официанту об ошибке
                                    call = cmd.CallWaiter(order.RestaurantID, order.TableID, order.OrderNumber, 4);
                                    //Запись сообщения об ошибке
                                    Messages msg = new Messages { RestaurantID = order.RestaurantID, TableID = order.TableID, MessageType = 4, MessageText = "Заказ оплачен на сумму " + order.OrderPayment.OrderSum.ToString() + " р. Сумма не совпадает", ErrorCode = error, ErrorText = error_msg };
                                    int save_msg = PersonalData.SqlInsertMessage(msg);
                                }
                                else
                                {
                                    //Ошибка: Заказ оплачен. Ошибка закрытия стола
                                    //Отправка сообщения официанту об ошибке
                                    call = cmd.CallWaiter(order.RestaurantID, order.TableID, order.OrderNumber, 5);
                                    //Запись сообщения об ошибке
                                    Messages msg = new Messages { RestaurantID = order.RestaurantID, TableID = order.TableID, MessageType = 5, MessageText = "Заказ оплачен на сумму " + order.OrderPayment.OrderSum.ToString() + " р. Ошибка закрытия стола", ErrorCode = error, ErrorText = error_msg };
                                    int save_msg = PersonalData.SqlInsertMessage(msg);
                                }
                            }

                            }
                            else
                            {
                                //Для ресторана невозможны онлайн оплаты

                                //Запись сообщения об оплате заказа
                                string tippingProc = (tip > 0) ? "Сервис - " + order.TippingProcent.ToString() + "%":"";
                                Messages msg = new Messages { RestaurantID = order.RestaurantID, TableID = order.TableID, MessageType = 8, MessageText = "Заказ оплачен на сумму " + order.OrderPayment.OrderSum.ToString() + " р." + tippingProc, ErrorCode = 0, ErrorText = "" };
                                int save_msg = PersonalData.SqlInsertMessage(msg);
                            }

                            return Redirect("http://185.26.113.204/success.html");
                            //return Redirect("http://test.veep.su/success.html");
                            //return Redirect("/success.html");
                        }
                    }
                    else
                    {
                        ret = response.ErrorMessage;
                    }
                }
                else
                {
                    ret = "Ошибка при подключении к платежной системе";
                    return Redirect("http://185.26.113.204/error.html");
                    //return Redirect("http://test.veep.su/error.html");
                }
            }
            else
            {
                ret = "Отсутствует номер заказа в платежной системе";
                return Redirect("http://185.26.113.204/error.html");
               // return Redirect("http://test.veep.su/error.html");
            }

            return Redirect("http://185.26.113.204/error.html");
            //return Redirect("http://test.veep.su/error.html");
            //return Redirect("/error.html");

        }

        public ActionResult PreOrder(string orderId)
        {
            string ret = "";
            if (!String.IsNullOrWhiteSpace(orderId))
            {
                Helper.saveToLog(0, "", "PreOrder", "orderId=" + orderId,"", 0);
                //Запрашиваем статус заказа в платежной системе
                orderStatusResponse response = new orderStatusResponse();
                response = MerchantData.getOrderStatus(orderId,1);
                if (response != null)
                {
                    //Вставка связки в БД
                    if (!String.IsNullOrEmpty(response.bindingId))
                    {
                        string bind = OrderData.SqlInsertBinding(response.clientId, response.bindingId, response.Pan, response.expiration, 1);
                    }
                     //Отменяем fake платеж
                    messageResponse msg = new messageResponse();
                    msg = MerchantData.reverse(orderId);
                    if (msg != null)
                    {
                        ret = msg.errorMessage;
                    }
                    else
                    {
                        ret = response.ErrorMessage;
                    }
                    if (!String.IsNullOrEmpty(response.bindingId))
                    {
                        return Redirect("http://185.26.113.204/success.html");
                        //return Redirect("http://test.veep.su/success.html");
                    }
                }
                else
                {
                    ret = "Ошибка при подключении к платежной системе";
                }
            }
            else
            {
                ret = "Отсутствует номер заказа в платежной системе";
            }
            return Redirect("http://185.26.113.204/error.html");
            //return Redirect("http://test.veep.su/error.html");
        }

        public ActionResult RedirectACS(int resraurantID, string orderId, string bindingId)
        {
            string res="";
            paymentOrderBindingResponse paymentResponse = new paymentOrderBindingResponse();
            paymentResponse = MerchantData.paymentOrderBinding(resraurantID, orderId, bindingId);
            if (paymentResponse != null)
            {
                    XMLGenerator<paymentOrderBindingResponse> resp = new XMLGenerator<paymentOrderBindingResponse>(paymentResponse);

                    if (paymentResponse.acsUrl != null)
                    {
                        string substr = "acs/";
                        int indexOfStr = (paymentResponse.acsUrl).IndexOf(substr);
                        string acsHost = (paymentResponse.acsUrl).Substring(0, indexOfStr);
                        res = MerchantData.paymentACS(orderId, paymentResponse);
                        res = res.Replace("acs/", acsHost + "acs/").Replace("/htt", "htt");
                        Helper.saveToLog(0, "", "GetPaymentBinding/paymentOrderBinding", "acsHost=" + acsHost + ", orderId=" + orderId + ", bindingId=" + bindingId, "Оплата заказа по связкам c ACS: paymentResponse=" + resp.GetStringXML(), 0);
                        return Content(res);
                    }
            }

            return Redirect("http://185.26.113.204/error.html");
           // return Redirect("http://test.veep.su/error.html");
        }

        protected void SendEmail(int restaurantID, string orderId)
        {
            MessageData data = new MessageData();
            if (!String.IsNullOrWhiteSpace(orderId))
            {
                data = OrderData.SqlMessageData(orderId);
                if (data != null && !String.IsNullOrWhiteSpace(data.clientEmail))
                {
                    string title = "";
                    string address = "";
                    switch (restaurantID)
                    {
                        case 202930001: /*Luce*/
                            title = "LUCE";
                            address = "<br/><br/>Общество с ограниченной ответственностью \"Альберо\".<br/>Адрес: Российская Федерация, 125047, г.Москва, улица 1-я Тверская-Ямская, дом 21.<br/>ИНН: 7703668115<br/>КПП: 771001001<br/><br/>";
                            break;
                        case 209631111: /*Vogue*/
                            title = "VOGUE Cafe";
                            address = "<br/><br/>Общество с ограниченной ответственностью \"Д.Т.С.-Порт\".<br/>Адрес: Российская Федерация, 107031, г.Москва, улица Кузнецктй мост, дом 7.<br/>ИНН: 7710343750<br/>КПП: 770701001<br/><br/>";
                            break;
                    }

                    StringBuilder sb = new StringBuilder();
                    //sb.Append("<br/><b>Уважаемый клиент!</b><br/><br/>Благодарим Вас за использование приложения VEEP!<br/><br/>Вы оплатили заказ в ресторане " + data.restaurantName + " банковской картой *" + data.pan + ".<br/><br/>");
                    sb.Append("<br/><b>Уважаемый клиент!</b><br/><br/>Благодарим Вас за посещение ресторана "+ title + ".<br/><br/>Вы оплатили заказ с помощью приложения VEEP.<br/><br/>");
                    sb.Append("<b>Информация о составе заказа:</b><br/>" + data.orderItems + "<br/><br/>");

                    //if (data.tipping > 0)
                   // {
                    //    sb.Append("Мы благодарим Вас за вознаграждение! Вы определили размер вознаграждения в " + data.tippingProcent + "% от стоимости заказа.<br/><br/>");
                   // }
                    sb.Append("<b>Информация о платеже:</b><br/>Стоимость заказа: " + data.orderTotal.ToString("0.00") + " р.<br/>Скидка: " + data.discountSum.ToString("0.00") + " р.<br/><br/>Сумма платежа: " + data.orderSum.ToString("0.00") + " р.<br/>");

                   // if (data.tipping > 0)
                   // {
                   //     sb.Append("Сумма чаевых: " + data.tipping.ToString("0.00") + " р.<br/>");
                   // }

                       // sb.Append("Сумма платежа : " + data.orderBank.ToString("0.00") + " р.<br/><br/>" + "Дата: " + data.orderDate + "<br/><br/>Ждем Вас в нашем ресторане!<br/><br/>");
                    sb.Append("Дата: " + data.orderDate + "<br/>Время: " + data.orderTime + "<br/><br/>Ждем Вас в нашем ресторане!<br/><br/>");
                        sb.Append("<hr width=\"100%\" size=\"1\">");
                        sb.Append(address);
                        sb.Append("<hr width=\"100%\" size=\"1\">");
                        sb.Append("<br/><br/>Если данное письмо попало к вам по ошибке, просто проигнорируйте его.<br/><br/>Это письмо было создано автоматически. Пожалуйста, не отвечайте на него. Если Вам требуется помощь или совет, вы хотите поделиться с нами полезной информацией — пишите нам на veep@novatorov.com. Ваше мнение очень важно для нас! Правила использования сервиса, Вы можете найти в Вашем мобильном приложении или на сайте <a href=\"mailto:veep.novatorov.com\">veep.novatorov.com</a>.<br/><br/>");
                    
                //Запрос к коммуникационному модулю на отправку email
                using (var smtpClient = new SmtpClient())
                {
                    string sourceEmail = "nr@novatorov.com";
                    string subject = "VEEP";
                    string messageText = sb.ToString(); 
                    string mail = data.clientEmail;

                    smtpClient.Host = "integrationapi.net";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = false;
                    smtpClient.Credentials = new NetworkCredential("novatorov", "DmE4Z7N9");

                    var msg = new MailMessage(sourceEmail, mail) { Sender = new MailAddress(sourceEmail), Subject = subject, Body = messageText, SubjectEncoding = System.Text.Encoding.Default, IsBodyHtml = true };
                    try
                    {
                        smtpClient.Send(msg);
                        Helper.saveToLog((int?)data.clientID, "", "SendEmail", "restaurantID=" + restaurantID.ToString() + ", orderId=" + orderId, "Отправлено письмо на адрес " + mail, 0);
                    }
                    catch (Exception ex)
                    {
                        Helper.saveToLog((int?)data.clientID, "", "SendEmail", "restaurantID=" + restaurantID.ToString() + ", orderId=" + orderId, "Ошибка при отправке письма на адрес " + mail + ". Ошибка: " + ex.Message, 1);
                    }
                }
                }
            }
            else
            {
                Helper.saveToLog((int?)data.clientID, "", "SendEmail", "restaurantID=" + restaurantID.ToString() + ", orderId=" + orderId, "Ошибка: Отсутствует номер заказа в платежной системе", 1);
            }

        }

        /*
        //Отправка сообщения на email
        public int SendMessage(int restaurantID, string orderNumber, string email, string message, string user_key)
        {
            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "" && email != "")
            {
                //Запрос к коммуникационному модулю на отправку email
                using (var smtpClient = new SmtpClient())
                {
                    var sourceEmail = "nr@novatorov.com";
                    var subject = "VEEP";
                    var messageText = message;
                    var mail = email;

                    smtpClient.Host = "integrationapi.net";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = false;
                    smtpClient.Credentials = new NetworkCredential("novatorov", "DmE4Z7N9");

                    var msg = new MailMessage(sourceEmail, mail) { Sender = new MailAddress(sourceEmail), Subject = subject, Body = messageText, SubjectEncoding = System.Text.Encoding.Default, IsBodyHtml = true };
                    try
                    {
                        smtpClient.Send(msg);
                        Helper.saveToLog(0, user_key, "SendMessage", "phoneNumber=" + phoneNumber, "Отправлено письмо на адрес " + mail, 0);
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.Message);
                        Helper.saveToLog(0, user_key, "SendMessage", "phoneNumber=" + phoneNumber, "Ошибка при отправке письма на адрес " + mail + ". Ошибка: " + ex.Message, 1);
                        return 0;
                    }
                }
            }
            return 0;
        }
        */


        //Оплата чаевых без связок
        public int GetPaymentTipping(string phoneCode, string phoneNumber, int restaurantID, string tableID, string orderNumber, string user_key, string clientId, string bindingId, string orderSum, decimal tippingSum, decimal tippingProcent, int waiterID, string waiterName)
        {
            //Номер заказа для банка для чаевых
            string num = Helper.getNewGUID();

            if (!String.IsNullOrWhiteSpace(bindingId))
            {
                //Оплата заказа с использованием связок

                //Регистрация заказа в платежной системе
                registerResponse response = new registerResponse();
                string returnUrl = "http://www.veep.su/success.html";
                //string returnUrl = "http://test.veep.su/success.html";
                string endPoint = @"https://" + Configs.MerchantHost + "register.do?";
                var client = new RestClient(endPoint);
                string user_name = Configs.GetMerchantUserTip(); //для боевого
                string psw = Configs.GetMerchantPswTip(); //для боевого
                //string user_name = Configs.GetMerchantUser(restaurantID); //для теста
                //string psw = Configs.GetMerchantPsw(restaurantID); //для теста

                string param = "amount=" + (tippingSum*100).ToString() + "&orderNumber=" + num + "&password=" + psw + "&returnUrl=" + returnUrl + "&userName=" + user_name + "&pageView=MOBILE&clientId=" + clientId;
                param += "&bindingId=" + bindingId;

                Helper.saveToLog(0, "", "GetPaymentTipping", "param=" + param, "", 0);

                var json = client.MakeRequest(param);
                registerResponse resp = new registerResponse();
                if (json != null)
                {
                    resp = JsonConvert.DeserializeObject<registerResponse>(json);
                    Helper.saveToLog(0, "", "GetPaymentTipping", "param=" + param, "Регистрация чаевых в платежной системе: errorCode=" + resp.errorCode.ToString() + ", errorMessage=" + resp.errorMessage + ", json=" + json, 0);
                }
                else
                {
                    Helper.saveToLog(0, "", "GetPaymentTipping", "param=" + param, "Ошибка при регистрации чаевых в платежной системе:  json=" + json, 1);
                }

                //********* оплата с SSL *********
                paymentOrderBindingResponse paymentResp = new paymentOrderBindingResponse();
                endPoint = @"https://" + Configs.MerchantHost + "paymentOrderBinding.do?";
                param = "mdOrder=" + resp.orderId + "&bindingId=" + bindingId + "&password=" + psw + "&userName=" + user_name;
                client = new RestClient(endPoint, HttpVerb.POST,param);
                json = client.MakeRequest(param,0);
                if (json != null)
                {
                    paymentResp = JsonConvert.DeserializeObject<paymentOrderBindingResponse>(json);
                }
                if (paymentResp != null)
                {
                    if (!String.IsNullOrWhiteSpace(paymentResp.redirect))
                    {
                        OrderData.SqlSaveTipping(phoneCode, phoneNumber, restaurantID, tableID, orderNumber, orderSum, tippingProcent, tippingSum, waiterID, waiterName);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }

            }

            return 0;
        }

        //Вывод списка платежей (админка)
        [HttpGet]
        public ActionResult Index()
        {

            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            // Фильтр
            Filters filter = new Filters();

            //Начальный диапазон дат
            filter.FilterBeginDate = ViewBag.beginDate = new DateTime(DateTime.Now.Year, 1, 1);
            filter.FilterEndDate = ViewBag.endDate = DateTime.Today;

            ViewBag.Filter = filter;
            Session["Filters"] = filter;

            ViewBag.Title = "Платежи";
            ViewBag.PageID = 5;

            return View();
        }

        [HttpPost]
        public ActionResult PaymentsLoad(Filters filter)
        {
            ViewBag.Filter = filter;
            Session["Filters"] = filter;
            ViewBag.Title = "Платежи";
            ViewBag.PageID = 5;

            DateTime begindate = (filter.FilterBeginDate.Year == 1) ? new DateTime(1900, 1, 1) : filter.FilterBeginDate;
            DateTime enddate = (filter.FilterEndDate.Year == 1) ? DateTime.Today : filter.FilterEndDate;
            TimeSpan time = new TimeSpan(23, 59, 59);
            enddate = enddate.Add(time);
            try
            {
                List<Payments> payments = new List<Payments>();
                payments = PaymentData.GetPaymentsList(begindate, enddate);
                if (payments != null)
                {
                    Session["payments"] = payments;
                    return PartialView(payments);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return null;
        }


        [HttpGet]
        public ActionResult PaymentExcel(string dfrom, string dto)
        {
            DateTime begindate = DateTime.Parse(dfrom);
            begindate = (begindate.Year == 1) ? new DateTime(1900, 1, 1) : begindate;
            DateTime enddate = DateTime.Parse(dto);
            enddate = (enddate.Year == 1) ? DateTime.Today : enddate;
            TimeSpan time = new TimeSpan(23, 59, 59);
            enddate = enddate.Add(time);

            try
            {
                var result = string.Empty;
                var wb = new Workbook();
                // properties
                wb.Properties.Created = DateTime.Today;

                // options sheets
                wb.ExcelWorkbook.ActiveSheet = 1;
                wb.ExcelWorkbook.DisplayInkNotes = false;
                wb.ExcelWorkbook.FirstVisibleSheet = 1;
                wb.ExcelWorkbook.ProtectStructure = false;
                wb.ExcelWorkbook.WindowHeight = 800;
                wb.ExcelWorkbook.WindowTopX = 0;
                wb.ExcelWorkbook.WindowTopY = 0;
                wb.ExcelWorkbook.WindowWidth = 600;

                // create style s1 for header
                var s1 = new Style("s1") { Font = new Font { Bold = true, Italic = true, Color = "#FF0000" } };
                wb.AddStyle(s1);

                // create style s2 for header
                var s2 = new Style("s2") { Font = new Font { Bold = true, Italic = true, Size = 12, Color = "#0000FF" } };
                wb.AddStyle(s2);

                // First sheet
                var ws = new Worksheet("Платежи");

                // Adding Headers
                ws.AddCell(0, 0, "Дата платежа", 0);
                ws.AddCell(0, 1, "Ресторан", 0);
                ws.AddCell(0, 2, "Телефон клиента", 0);
                ws.AddCell(0, 3, "Номер заказа в ИМ", 0);
                ws.AddCell(0, 4, "Номер заказа в сервисе", 0);
                ws.AddCell(0, 5, "Номер заказа в банке", 0);
                ws.AddCell(0, 6, "Дата регистр. в банке", 0);
                ws.AddCell(0, 7, "Сумма платежа, руб.", 0);
                ws.AddCell(0, 8, "Результат платежа", 0);
                ws.AddCell(0, 9, "Карта", 0);
                ws.AddCell(0, 10, "ID связки", 0);

                // get data
                List<Payments> list = new List<Payments>();
                list = PaymentData.GetPaymentsList(begindate, enddate);

                if (list != null)
                {
                    int n = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        ws.AddCell(i + n + 1, 0, (list[i].PaymentDate.Year > 1970) ? (list[i].PaymentDate.ToShortDateString() + " " + list[i].PaymentDate.ToShortTimeString()) : "", 0);
                        ws.AddCell(i + n + 1, 1, list[i].RestaurantName, 0);
                        ws.AddCell(i + n + 1, 2, list[i].PhoneNumber, 0);
                        ws.AddCell(i + n + 1, 3, list[i].OrderNumberModule, 0);
                        ws.AddCell(i + n + 1, 4, list[i].OrderNumber, 0);
                        ws.AddCell(i + n + 1, 5, list[i].OrderNumberBank, 0);
                        ws.AddCell(i + n + 1, 6, ((list[i].RegDate.Year > 1970) ? (list[i].RegDate.ToShortDateString() + " " + list[i].RegDate.ToShortTimeString()) : ""), 0);
                        ws.AddCell(i + n + 1, 7, ((Convert.ToDecimal(list[i].OrderSumBank) / 100).ToString("0.00")), 0);
                        ws.AddCell(i + n + 1, 8, list[i].ErrorMessage, 0);
                        ws.AddCell(i + n + 1, 9, (list[i].Pan + " / " + list[i].Expiration + " / " + list[i].CardHolderName), 0);
                        ws.AddCell(i + n + 1, 10, list[i].BindingID, 0);
                    }
                    wb.AddWorksheet(ws);

                    // generate xml 
                    var workstring = wb.ExportToXML();

                    // Send to user file
                    return new ExcelResult("Payments.xls", workstring);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return null;
        }

    }
}