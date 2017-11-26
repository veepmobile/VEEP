using System;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Text.RegularExpressions;
using HttpUtils;
using RestService.BLL;
using RestService.Models;
using RestService.CommService;

namespace RestService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Rest : IRest
    {
        #region Account

        //Поиск аккаунта по номеру телефона при старте Приложения
        public Account FindAccount(string phoneNumber, string phoneCode = "7", int language = 0)
        {
            Account account = new Account();
            if (!String.IsNullOrWhiteSpace(phoneNumber))
            {
                if (phoneCode == "") { phoneCode = "7"; }

                account = AccountData.SqlFindAccount(phoneNumber, phoneCode, language);

                if (account.AccountStatus != null && (account.AccountStatus.ID == 4 || account.AccountStatus.ID == 5))
                {
                    //account.Message = "Ваш профиль заблокирован. Обратитесь в службу поддержки +79175074223.";
                    account.Message = Helper.GetError(1, language);
                    XMLGenerator<Account> accountXML = new XMLGenerator<Account>(account);
                    Helper.saveToLog(0, "", "FindAccount", "phoneNumber=" + phoneNumber, "Аккаунт заблокирован: " + accountXML.GetStringXML(), 1);
                }
                //return AccountData.SqlFindAccount(phoneNumber);
            }
            else
            {
                //account.Message = "Неправильный номер телефона";
                account.Message = Helper.GetError(3, language);
                XMLGenerator<Account> accountXML = new XMLGenerator<Account>(account);
                Helper.saveToLog(0, "", "FindAccount", "phoneNumber=" + phoneNumber, "Аккаунт не найден: " + accountXML.GetStringXML(), 1);
            }
            return account;
        }

        //Поиск clientId по номеру телефона
        protected string GetClientId(string phoneNumber, string phoneCode = "7")
        {
            if (phoneCode == "") { phoneCode = "7"; }
            string clientId = "";
            if (!String.IsNullOrWhiteSpace(phoneNumber))
            {
                clientId = AccountData.SqlFindClientId(phoneNumber, phoneCode);
            }

            return clientId;
        }

        //Регистрация аккаунта
        public Account RegistrAccount(string FirstName, string LastName, string PhoneModel, string PhoneNumber, string PhoneUID, int? PhoneOS_ID, string Pswd, string Email, string PhoneCode = "7", int language = 0)
        {
            //test
            //Helper.saveToLog(0, "", "RegistrAccount", "PhoneCode=" + PhoneCode, "", 1);
            Account account = new Account();
            account.FirstName = !String.IsNullOrWhiteSpace(FirstName) ? FirstName : "";
            account.LastName = !String.IsNullOrWhiteSpace(LastName) ? LastName : "";
            account.PhoneModel = !String.IsNullOrWhiteSpace(PhoneModel) ? PhoneModel : "";
            account.PhoneCode = !String.IsNullOrWhiteSpace(PhoneCode) ? PhoneCode : "7";
            account.PhoneNumber = !String.IsNullOrWhiteSpace(PhoneNumber) ? PhoneNumber : "";
            account.PhoneUID = !String.IsNullOrWhiteSpace(PhoneUID) ? PhoneUID : "";
            PhoneOS os = new PhoneOS();
            os.ID = (PhoneOS_ID != null) ? (int)PhoneOS_ID : 1;
            account.PhoneOS = os;
            account.Pswd = !String.IsNullOrWhiteSpace(Pswd) ? Pswd : "";
            account.Email = !String.IsNullOrWhiteSpace(Email) ? Email : "";
            if (account != null)
            {
                return AccountData.SqlRegistrAccount(account, language);
            }
            //account.Message = "Ошибка при регистрации аккаунта";
            account.Message = Helper.GetError(100, language); //что-то пошло не так
            XMLGenerator<Account> accountXML = new XMLGenerator<Account>(account);
            Helper.saveToLog(0, "", "RegistrAccount", "account=" + accountXML.GetStringXML(), "Ошибка при регистрации аккаунта", 1);
            return account;
        }

        //Проверка введенного кода SMS
        public bool CheckSMS(string phoneNumber, string SMScode, string phoneCode = "7")
        {
            if (phoneCode == "") { phoneCode = "7"; }
            bool check = false;
            if (!String.IsNullOrEmpty(SMScode) && !String.IsNullOrEmpty(phoneNumber))
            {
                check = AccountData.SqlCheckSMS(phoneNumber, phoneCode, SMScode);
            }
            return check;
        }

        //Изменение статуса аккаунта
        public int UpdateAccountStatus(int accountID, int statusID)
        {
            return AccountData.SqlUpdateStatus(accountID, statusID);
        }

        //Авторизация
        public string Login(string phoneNumber, string psw, string phoneCode = "7")
        {
            phoneNumber = phoneNumber.Trim().Replace("'", "").Replace(" ", "");
            psw = psw.Trim().Replace("'", "").Replace(" ", "");
            if (phoneCode == "") { phoneCode = "7"; }
            string user_key = "";

            //проверка логина/пароля
            bool check = AccountData.SqlLogin(phoneNumber, psw, phoneCode);

            if (check)
            {
                //проверка открытой сессии
                user_key = AccountData.SqlCheckSession(phoneNumber, phoneCode);
                if (user_key != "")
                {
                    //возвращаем user_key
                    Helper.saveToLog(0, user_key, "Login", "param: phoneNumber=" + phoneNumber + ", phoneCode=" + phoneCode + ", psw=" + psw, "Аккаунт авторизован, открыта сессия с user_key = " + user_key, 0);
                    return user_key;
                }
                else
                {
                    //нет открытой сессии - открываем сессию
                    user_key = AccountData.SqlInsertSession(phoneNumber, phoneCode);
                    if (user_key != "")
                    {
                        Helper.saveToLog(0, user_key, "Login", "param: phoneNumber=" + phoneNumber + ", phoneCode=" + phoneCode + ", psw=" + psw, "Аккаунт авторизован, открыта сессия с user_key = " + user_key, 0);
                        return user_key;
                    }
                }

            }
            Helper.saveToLog(0, "", "Login", "param: phoneNumber=" + phoneNumber + ", phoneCode=" + phoneCode + ", psw=" + psw, "Неудачная авторизация", 1);
            return user_key;
        }

        //Проверка открытой сессии
        public string CheckSession(string phoneNumber, string phoneCode = "7")
        {
            if (phoneCode == "") { phoneCode = "7"; }
            string user_key = AccountData.SqlCheckSession(phoneNumber, phoneCode);
            if (user_key != "")
            {
                return user_key; //сессия открыта
            }
            return null;
        }

        //Проверка открытой сессии по user_key
        private string CheckUserKey(string user_key)
        {
            string result = AccountData.SqlCheckKey(user_key);
            if (result != "")
            {
                return result;
            }
            return "";
        }

        //Блокировка аккаунта и завершение открытой сессии
        public int BlockedAccount(string phoneNumber, int block_id, string phoneCode = "7")
        {
            if (phoneCode == "") { phoneCode = "7"; }
            Exit(phoneNumber, null, phoneCode);
            int result = AccountData.BlockAccount(phoneNumber, phoneCode, block_id);

            return result;
        }

        //Обновление данных аккаунта
        public Account UpdateAccount(string FirstName, string LastName, string PhoneModel, string PhoneNumber, string PhoneUID, int? PhoneOS_ID, string Email, string user_key, string PhoneCode = "7", int language = 0)
        {
            if (PhoneCode == "") { PhoneCode = "7"; }
            if (!String.IsNullOrWhiteSpace(PhoneNumber))
            {
                Account account = new Account();
                account = AccountData.SqlFindAccount(PhoneNumber, PhoneCode);
                if (account != null && CheckUserKey(user_key) != "")
                {
                    Account new_account = new Account();
                    new_account.ID = account.ID;
                    new_account.FirstName = !String.IsNullOrWhiteSpace(FirstName) ? FirstName : account.FirstName;
                    new_account.LastName = !String.IsNullOrWhiteSpace(LastName) ? LastName : account.LastName;
                    new_account.PhoneModel = !String.IsNullOrWhiteSpace(PhoneModel) ? PhoneModel : account.PhoneModel;
                    new_account.PhoneNumber = !String.IsNullOrWhiteSpace(PhoneNumber) ? PhoneNumber : account.PhoneNumber;
                    new_account.PhoneCode = !String.IsNullOrWhiteSpace(PhoneCode) ? PhoneCode : account.PhoneCode;
                    new_account.PhoneUID = !String.IsNullOrWhiteSpace(PhoneUID) ? PhoneUID : account.PhoneUID;
                    PhoneOS os = new PhoneOS();
                    os.ID = (PhoneOS_ID != null) ? (int)PhoneOS_ID : 1;
                    new_account.PhoneOS = os;
                    new_account.Email = !String.IsNullOrWhiteSpace(Email) ? Email : "";
                    AccountStatus status = new AccountStatus();
                    status.ID = account.AccountStatus.ID;
                    status.Name = account.AccountStatus.Name;
                    new_account.AccountStatus = status;
                    return AccountData.SqlUpdateAccount(new_account, language);
                }
            }
            return null;
        }

        //Смена пароля
        public int ChangePsw(string phoneNumber, string oldPsw, string newPsw, string user_key, string phoneCode = "7")
        {
            if (phoneCode == "") { phoneCode = "7"; }
            int result = 0;
            string phone_number = CheckUserKey(user_key);
            if (phone_number != "" && phone_number == phoneNumber)
            {
                result = AccountData.SqlChangePsw(phoneNumber, phoneCode, user_key, oldPsw, newPsw);
                return result;
            }
            return result;
        }

        //Завершение сессии
        public int Exit(string phoneNumber, string user_key, string phoneCode = "7")
        {
            if (phoneCode == "") { phoneCode = "7"; }
            return AccountData.Exit(phoneNumber, phoneCode, user_key);
        }

        #endregion

        #region DiscountCard

        //Загрузка привязанных карт в Приложение
        public List<Models.DiscountCard> FindDiscountCard(string phoneNumber, string user_key, string phoneCode = "7")
        {
            if (!String.IsNullOrWhiteSpace(phoneNumber) && CheckUserKey(user_key) != "")
            {
                string check = CheckSession(phoneNumber); //проверка открытой сессии
                if (check != "")
                {
                    int accountID = Convert.ToInt32(AccountData.SqlFindClientId(phoneNumber, phoneCode));
                    return DiscountCardData.SqlFindDiscountCard(accountID, check);
                    //return DiscountCardData.SqlFindDiscountCard(phoneNumber,check);
                }
            }
            return null;
        }

        //Привязка дисконтной карты в Системе
        public int InsertDiscountCard(string phoneNumber, long cardNumber, string cardName, string user_key, string phoneCode = "7", int language = 0)
        {
            int ret = 0; // "Карта не привязана";
            int ID = 0;
            if (!String.IsNullOrWhiteSpace(phoneNumber) && cardNumber != 0 && CheckUserKey(user_key) != "")
            {
                string check = CheckSession(phoneNumber); //проверка открытой сессии
                if (check != "")
                {
                    int accountID = Convert.ToInt32(AccountData.SqlFindClientId(phoneNumber, phoneCode));
                    ID = DiscountCardData.SqlInsertDiscountCard(accountID, cardNumber, cardName, user_key);
                    if (ID < 0)
                    {
                        ret = 1; // "Карта уже привязана другим пользователем";
                    }
                    if (ID > 0)
                    {
                        ret = 2; // "Для подтверждения привязки при посещении ресторана авторизуйтесь в заказе и предъявите дисконтную карту официанту. Обратите внимание карту можно привязать только к одному профилю!";
                    }
                }
            }
            return ret;
        }


        //Изменение статуса (удаление) дисконтной карты в Системе
        public long? UpdateDiscountCard(string phoneNumber, long? cardNumber, int cardStatus, string user_key, string phoneCode = "7", int language = 0)
        {
            if (!String.IsNullOrWhiteSpace(phoneNumber) && cardNumber != 0 && CheckUserKey(user_key) != "")
            {
                string check = CheckSession(phoneNumber); //проверка открытой сессии
                if (check != "")
                {
                    int accountID = Convert.ToInt32(AccountData.SqlFindClientId(phoneNumber, phoneCode));
                    return DiscountCardData.SqlUpdateDiscountCard(accountID, user_key, cardNumber, cardStatus);
                }
            }
            Helper.saveToLog(0, user_key, " UpdateDiscountCard", "phoneNumber: " + phoneNumber + ", cardNumber: " + cardNumber.ToString() + ", cardStatus: " + cardStatus.ToString(), "", 0);
            return 0;
        }

        //Редактирование названия дисконтной карты
        public int UpdateDiscountCardName(string phoneNumber, long cardNumber, string cardName, string user_key, string phoneCode = "7", int language = 0)
        {
            if (!String.IsNullOrWhiteSpace(phoneNumber) && cardNumber != 0 && CheckUserKey(user_key) != "")
            {
                string check = CheckSession(phoneNumber); //проверка открытой сессии
                if (check != "")
                {
                    int accountID = Convert.ToInt32(AccountData.SqlFindClientId(phoneNumber, phoneCode));
                    return DiscountCardData.SqlUpdateDiscountCardName(accountID, user_key, cardNumber, cardName);
                }
            }
            return 0;
        }

        //Применение/проверка дисконтной карты
        public int CheckDiscountCard(string phoneNumber, int restaurantID, string orderNumber, string user_key, long? discountCard, string phoneCode = "7", int language = 0)
        {
            Helper.saveToLog(0, user_key, " CheckDiscountCard - пришло", "phoneNumber: " + phoneNumber + ", restaurant_id: " + restaurantID.ToString() + ", discountCard: " + discountCard.ToString(), "", 0);
            List<Order> list = new List<Order>();
            list = GetOrderDiscount(restaurantID, orderNumber, user_key, phoneCode, language, discountCard);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.DiscountCard != null && item.DiscountCard.CardStatus == 1)
                    {
                        long? result_status = UpdateDiscountCard(phoneNumber, discountCard, 1, user_key, phoneCode, language);
                        return 1; // "Скидка применена";
                    }
                }
            }

            return 0; // "Скидка не применилась. Повторите попытку или обратитесь к официанту";
        }

        #endregion

        #region Bindings

        //Запрос списка возможных связок
        public List<Binding> GetBindings(string phoneNumber, string user_key, string phoneCode = "7")
        {
            if (CheckUserKey(user_key) != "")
            {
                if (phoneCode == "") { phoneCode = "7"; }
                List<Binding> list = new List<Binding>();
                string clientId = GetClientId(phoneNumber, phoneCode);

                if (!String.IsNullOrWhiteSpace(clientId))
                {
                    //Поиск связок с БД
                    list = OrderData.SqlFindBindings(clientId);
                    XMLGenerator<List<Binding>> listXML = new XMLGenerator<List<Binding>>(list);
                    Helper.saveToLog(Int32.Parse(clientId), user_key, "GetBindings", "phoneNumber=" + phoneNumber, "Найдены привязки: " + listXML.GetStringXML(), 0);
                    return list;

                    /*
                    //Поиск привязок в сбере
                    getBindingsResponse response = new getBindingsResponse();
                    response = MerchantData.getBindings(clientId);
                    Helper.saveToLog(0, user_key, "GetBindings", "phoneNumber=" + phoneNumber + ", clientId=" + clientId, "Получение списка связок: errorCode=" + response.errorCode.ToString() + ", errorMessage=" + response.errorMessage, 0);
                    list = response.bindings;
                    if (list != null)
                    {
                        foreach (var item in list)
                        {
                            item.clientId = clientId;
                            //Запись/обновление связки в табл.Bindings
                            if (!String.IsNullOrWhiteSpace(item.bindingId))
                            {
                                string bind_result = OrderData.SqlInsertBinding(item.clientId, item.bindingId, item.maskedPan, item.expiryDate, 1);
                            }
                        }
                        return list;
                    }
                    */
                }
                Helper.saveToLog((clientId != "") ? Int32.Parse(clientId) : 0, user_key, "GetBindings", "phoneNumber=" + phoneNumber, "Привязки не найдены", 1);
                return null;
            }

            return null;
        }

        //Редактирование названия карты
        public List<Binding> ChangeCardName(string phoneNumber, string bindingId, string cardName, string user_key, string phoneCode = "7", int language = 0)
        {
            if (CheckUserKey(user_key) != "")
            {
                if (phoneCode == "") { phoneCode = "7"; }
                List<Binding> list = new List<Binding>();
                string clientId = GetClientId(phoneNumber, phoneCode);

                if (!String.IsNullOrWhiteSpace(clientId))
                {
                    //Обновление названия карты
                    int result = OrderData.SqlChangeCardName(Int32.Parse(clientId), bindingId, cardName);
                    if (result == 0)
                    {
                        Helper.saveToLog(Int32.Parse(clientId), user_key, "ChangeCardName", "phoneNumber=" + phoneNumber + ", bindingId=" + bindingId + ", cardName=" + cardName, "Ошибка при изменении названия карты", 1);
                    }

                    //Поиск связок с БД
                    list = OrderData.SqlFindBindings(clientId);
                    XMLGenerator<List<Binding>> listXML = new XMLGenerator<List<Binding>>(list);
                    Helper.saveToLog(Int32.Parse(clientId), user_key, "GetBindings", "phoneNumber=" + phoneNumber, "Найдены привязки: " + listXML.GetStringXML(), 0);
                    return list;
                }
                Helper.saveToLog(Int32.Parse(clientId), user_key, "ChangeCardName", "phoneNumber=" + phoneNumber + ", bindingId=" + bindingId + ", cardName=" + cardName, "Привязки не найдены", 1);
                return null;
            }

            return null;
        }

        //Предварительная привязка карты
        public string RegisterBinding(string user_key, string phoneCode = "7", int language = 0)
        {
            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "")
            {
                if (phoneCode == "") { phoneCode = "7"; }
                string clientId = GetClientId(phoneNumber, phoneCode);
                //Регистрация двухстадийного платежа в платежной системе
                registerResponse response = new registerResponse();
                //string returnUrl = "http://185.26.113.204/Payment/PreOrder";
                string returnUrl = "http://test.veep.su/Payment/PreOrder";
                response = MerchantData.registerPreOrder(Helper.getNewGUID(), 200, returnUrl, clientId, "");
                if (response != null)
                {
                    XMLGenerator<registerResponse> responseXML = new XMLGenerator<registerResponse>(response);
                    Helper.saveToLog(0, "", "RegisterBinding", "phoneNumber=" + phoneNumber + ", clientId=" + clientId, "Регистрация привязки: " + responseXML.GetStringXML(), 0);
                    //return (!String.IsNullOrWhiteSpace(response.formUrl)) ? response.formUrl : response.errorMessage;
                    return (!String.IsNullOrWhiteSpace(response.formUrl)) ? response.formUrl : Helper.GetError(100, language);
                }
                else
                { Helper.saveToLog(0, "", "RegisterBinding", "phoneNumber=" + phoneNumber + ", clientId=" + clientId, "привязка не зарегистрирована.", 1); }

            }

            return null;
        }

        //Деактивация связки
        public string UnbindCard(string bindingId, string user_key, int language = 0)
        {
            if (CheckUserKey(user_key) != "")
            {
                if (!String.IsNullOrWhiteSpace(bindingId))
                {
                    messageResponse response = new messageResponse();
                    response = MerchantData.unbindCard(bindingId);
                    if (response != null)
                    {
                        if (response.errorCode == 0)
                        {
                            //Удаляем привязку карты из БД
                            string unbind_result = OrderData.SqlUnbindCard(bindingId);
                        }
                        //return response.errorMessage;
                        return Helper.GetError(100, language);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //return "Отсутствует ID связки";
                    return Helper.GetError(100, language);
                }
            }

            return null;
        }

        //Aктивация связки
        public string BindCard(string bindingId, string user_key, int language = 0)
        {
            if (CheckUserKey(user_key) != "")
            {
                if (!String.IsNullOrWhiteSpace(bindingId))
                {
                    messageResponse response = new messageResponse();
                    response = MerchantData.bindCard(bindingId);
                    if (response != null)
                    {
                        //return response.errorMessage;
                        return Helper.GetError(100, language);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    // return "Отсутствует ID связки";
                    return Helper.GetError(100, language);
                }
            }

            return null;
        }

        #endregion

        #region Orders

        //Получение информации о заказах стола, привязанных к блюду
        public List<Order> FindOrders(string phoneNumber, string qr, string user_key, string phoneCode = "7", int language = 0)
        {
            if (CheckUserKey(user_key) != "")
            {
                /*
                //Проверка QR кода
                 if (OrderData.SqlCheckQR(qr))
                 {
                     List<Order> list = new List<Order>();
                     Order order = new Order();
                     order.ErrorCode = 1;
                     order.Error = "Данный QR код уже использовался";
                     list.Add(order);
                     return list;
                 }
                 else
                 {
                     OrderData.SqlSaveQR(qr);
                 }
                */

                //Парсим QR код
                int rest = Int32.Parse(qr.Substring(0, 6));
                int techItem = Int32.Parse(qr.Substring(9, 3));

                int restaurantID = RestaurantData.GetRestaurantID(rest);

                //Заглушка для теста 
                techItem = 22;
                restaurantID = 730410002; //тестовый

                /* При первоначальном поиске заказа дисконтной карты еще нет
                //Получение номера привязанной дисконтной карты
                DiscountCard card = new DiscountCard();
                card.CardNumber = DiscountCardData.SqlGetDiscountCard(phoneNumber, user_key, restaurantID);
                //Запрос к Интеграционному модулю
                Helper.saveToLog(0, user_key, " IntegrationCMD.FindOrders", "restaurant_id: " + restaurantID.ToString() + ", techItem: " + techItem.ToString() + ", card_number: " + card.CardNumber.ToString(),"", 0);
                 */

                string endpointName = "";
                string address = "";
                endpointName = Configs.GetEndpoint(restaurantID);
                address = Configs.GetAddress(restaurantID);

                IntegrationCMD.IntegrationCMDClient cmd = new IntegrationCMD.IntegrationCMDClient(endpointName, address);
                //IntegrationCMD.Order[] orders = cmd.FindOrders(restaurantID, techItem, card.CardNumber);
                IntegrationCMD.Order[] orders = cmd.FindOrders(restaurantID, techItem, null);

                string tableId = "";
                if (orders != null)
                {
                   //Проверка кода ошибки 43 и 44
                    foreach (var item in orders)
                    {
                        Helper.saveToLog(0, user_key, " IntegrationCMD.FindOrders", "restaurant_id: " + restaurantID.ToString() + ", techItem: " + techItem.ToString() + ", ErrorCode: " + item.ErrorCode.ToString(), "", 0);
                        if (item.ErrorCode == 43 || item.ErrorCode == 44)
                        {
                            //orders = cmd.FindOrdersFull(restaurantID, techItem, card.CardNumber);
                            orders = cmd.FindOrdersFull(restaurantID, techItem, null);
                            Helper.saveToLog(0, user_key, " IntegrationCMD.FindOrdersFull", "restaurant_id: " + restaurantID.ToString() + ", techItem: " + techItem.ToString() + ", ErrorCode: " + item.ErrorCode.ToString(), "", 0);
                            break;
                        }

                    }

                    List<Order> list = new List<Order>();
                    foreach (var item in orders)
                    {
                        Helper.saveToLog(0, user_key, " IntegrationCMD.FindOrders", "restaurant_id: " + restaurantID.ToString() + ", techItem: " + techItem.ToString() + ", ErrorCode: " + item.ErrorCode.ToString(), "", 0);

                        Order order = new Order();
                        order.OrderNumber = item.OrderNumber;
                        order.RestaurantID = item.RestaurantID;
                        order.TableID = item.TableID;
                        tableId = item.TableID;
                        if (item.Waiter != null)
                        {
                            Waiter waiter = new Waiter();
                            waiter.ID = item.Waiter.ID;
                            waiter.Name = item.Waiter.Name;
                            order.Waiter = waiter;
                        }
                        if (item.Items != null)
                        {
                            List<OrderItem> list_items = new List<OrderItem>();
                            foreach (var order_item in item.Items)
                            {
                                OrderItem list_item = new OrderItem();
                                list_item.Name = order_item.Name;
                                list_item.Qty = order_item.Qty;
                                list_item.Price = order_item.Price;
                                list_items.Add(list_item);
                            }
                            order.OrderItems = list_items;
                        }
                        if (item.OrderStatus != null)
                        {
                            OrderStatus os = new OrderStatus();
                            os.StatusID = item.OrderStatus.StatusID;
                            os.StatusDate = item.OrderStatus.StatusDate;
                            order.OrderStatus = os;
                        }
                        if (item.OrderPayment != null)
                        {
                            OrderPayment op = new OrderPayment();
                            op.OrderTotal = item.OrderPayment.OrderTotal;
                            op.DiscountSum = item.OrderPayment.DiscountSum;
                            op.OrderSum = item.OrderPayment.OrderSum;
                            op.OrderBank = 0;
                            order.OrderPayment = op;
                        }

                        //Скидка по дисконтной карте
                        if(item.DiscountCard != null)
                        {
                            DiscountCard discard = new DiscountCard();
                            discard.CardNumber = item.DiscountCard.CardNumber;
                            discard.CardStatus = item.DiscountCard.CardStatus;
                            discard.LastDate = item.DiscountCard.LastDate;
                            order.DiscountCard = discard;
                        }

                        order.Message = item.Message;
                        order.ErrorCode = item.ErrorCode;
                        order.Error = item.Error;

                        list.Add(order);

                        if (order.ErrorCode == 31)
                        {
                            order.Error = "Официант редактирует заказ. Повторите запрос через минуту.";
                            Helper.saveToLog(0, user_key, "FindOrders", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + order.TableID + ", ErrorCode: " + item.ErrorCode.ToString(), "Официант работает с заказом", 1);
                            return list;
                        }
                        //Проверка оплаты заказа в БД
                        if (OrderData.GetStatus(order.OrderNumber) == 2)
                        {
                            order.ErrorCode = 5;
                            //order.Error = "Вы уже оплатили данный заказ. Для открытия нового заказа обратитесь к официанту.";
                            order.Error = Helper.GetError(5, language);
                            Helper.saveToLog(0, user_key, "FindOrders", "restaurant_id: " + restaurantID.ToString() + ", techItem: " + techItem.ToString() + ", ErrorCode: ", "Заказ уже оплачен", 1);
                            return list;
                        }
                        //Проверка закрытия заказа на кассе
                        if (order.OrderStatus != null)
                        {
                            if (order.OrderStatus.StatusID == 2)
                            {
                                order.ErrorCode = 6;
                                //order.Error = "Ваш заказ закрыт. Для открытия нового заказа обратитесь к официанту."; 
                                order.Error = Helper.GetError(6, language);
                                Helper.saveToLog(0, user_key, "FindOrders", "restaurant_id: " + restaurantID.ToString() + ", techItem: " + techItem.ToString() + ", StatusID: " + order.OrderStatus.StatusID.ToString(), "Заказ закрыт", 1);
                                return list;
                            }
                        }

                        if (order.ErrorCode != 0)
                        {
                            order.ErrorCode = 100;
                            order.Error = Helper.GetError(100, language);
                            return list;
                        }

                        //Запись заказа в БД
                        OrderData.SqlInsertOrders(restaurantID, phoneNumber, user_key, order);

                        //Скидка veep
                        foreach (var o in list)
                        {
                            o.MainDiscountProc = RestaurantData.GetVeepDiscount(restaurantID); ;
                            o.MainDiscountSum = o.OrderPayment.OrderSum * o.MainDiscountProc / 100;
                            o.OrderPayment.OrderSum = o.OrderPayment.OrderSum - o.MainDiscountSum;
                        }

                    }

                    XMLGenerator<List<Order>> listXML = new XMLGenerator<List<Order>>(list);
                    Helper.saveToLog(0, user_key, "FindOrders", "phoneNumber=" + phoneNumber + ", qr=" + qr, "Найдены заказы: " + listXML.GetStringXML(), 0);

                    //Запись сообщения о регистрации за столом
                    if (tableId != "")
                    {
                        Messages msg = new Messages { RestaurantID = restaurantID, TableID = tableId, MessageType = 101, MessageText = "Регистрация за столом" };
                        int save_msg = PersonalData.SqlInsertMessage(msg);
                    }
                    return list;
                }
                else
                {
                    Helper.saveToLog(0, user_key, "FindOrders", "phoneNumber=" + phoneNumber + ", qr=" + qr, "Заказы не найдены", 1);
                    return null;
                }
            }
            Helper.saveToLog(0, user_key, "FindOrders", "phoneNumber=" + phoneNumber + ", qr=" + qr, "Заказы не найдены", 1);
            return null;
        }

        //Получение информации о заказе по его номеру
        public List<Order> GetOrderDiscount(int restaurantID, string orderNumber, string user_key, string phoneCode = "7", int language = 0, long? discountCard = null)
        {
            string phoneNumber = CheckUserKey(user_key);
            Helper.saveToLog(0, user_key, "GetOrderDiscount - пришло из приложения", "restaurant_id: " + restaurantID.ToString() + ", orderNumber=" + orderNumber + ", discountCard=" + discountCard.ToString(), "", 0);
            if (phoneNumber != "")
            {
                //Запрос к Интеграционному модулю
                string endpointName = "";
                string address = "";

                endpointName = Configs.GetEndpoint(restaurantID);
                address = Configs.GetAddress(restaurantID);

                IntegrationCMD.IntegrationCMDClient cmd = new IntegrationCMD.IntegrationCMDClient(endpointName, address);
                
                //discountCard = 1001;

                Helper.saveToLog(0, user_key, "GetOrderDiscount - отправлено в модуль", "restaurant_id: " + restaurantID.ToString() + ", orderNumber=" + orderNumber + ", discountCard=" + discountCard.ToString(), "", 0);
                IntegrationCMD.Order[] orders = cmd.GetOrder(restaurantID, orderNumber, discountCard);
                if (orders != null)
                {
                    List<Order> list = new List<Order>();
                    foreach (var item in orders)
                    {
                        Order order = new Order();
                        order.OrderNumber = item.OrderNumber;
                        order.RestaurantID = item.RestaurantID;
                        order.TableID = item.TableID;
                        if (item.Waiter != null)
                        {
                            Waiter waiter = new Waiter();
                            waiter.ID = item.Waiter.ID;
                            waiter.Name = item.Waiter.Name;
                            order.Waiter = waiter;
                        }
                        if (item.Items != null)
                        {
                            List<OrderItem> list_items = new List<OrderItem>();
                            foreach (var order_item in item.Items)
                            {
                                OrderItem list_item = new OrderItem();
                                list_item.Name = order_item.Name;
                                list_item.Qty = order_item.Qty;
                                list_item.Price = order_item.Price;
                                list_items.Add(list_item);
                            }
                            order.OrderItems = list_items;
                        }
                        if (item.OrderStatus != null)
                        {
                            OrderStatus os = new OrderStatus();
                            os.StatusID = item.OrderStatus.StatusID;
                            os.StatusDate = item.OrderStatus.StatusDate;
                            order.OrderStatus = os;
                        }
                        if (item.OrderPayment != null)
                        {
                            OrderPayment op = new OrderPayment();
                            op.OrderTotal = item.OrderPayment.OrderTotal;
                            op.DiscountSum = item.OrderPayment.DiscountSum;
                            op.OrderSum = item.OrderPayment.OrderSum;
                            op.OrderBank = 0;
                            order.OrderPayment = op;
                        }

                        //Скидка по дисконтной карте
                        if (item.DiscountCard != null)
                        {
                            DiscountCard discard = new DiscountCard();
                            discard.CardNumber = (long?)item.DiscountCard.CardNumber;
                            discard.CardStatus = item.DiscountCard.CardStatus;
                            discard.LastDate = item.DiscountCard.LastDate;
                            order.DiscountCard = discard;
                        }
                        //                        Helper.saveToLog(0, user_key, "GetOrderDiscount пришло", "DiscountCard.CardNumber: " + item.DiscountCard.CardNumber.ToString() + ", item.DiscountCard.CardStatus: " + item.DiscountCard.CardStatus.ToString() + ", item.DiscountCard.LastDate: " + item.DiscountCard.LastDate.ToShortDateString(), "", 0);
                        order.Message = item.Message;
                        order.Error = item.Error;
                        order.ErrorCode = item.ErrorCode;
                        list.Add(order);

                        Helper.saveToLog(0, user_key, "GetOrderDiscount", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + order.TableID + ", ErrorCode: " + item.ErrorCode.ToString(), "", 1);

                        //Проверка ошибки 31 - официант работает с заказом
                        if (item.ErrorCode == 31)
                        {
                            order.ErrorCode = 31;
                            //order.Error = "Официант редактирует заказ. Повторите запрос через минуту.";
                            order.Error = Helper.GetError(31, language);
                            Helper.saveToLog(0, user_key, "GetOrderDiscount", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + order.TableID + ", ErrorCode: " + item.ErrorCode.ToString(), "Официант работает с заказом", 1);
                            return list;
                        }
                        //Проверка оплаты заказа в БД
                        if (OrderData.GetStatus(order.OrderNumber) == 2)
                        {
                            order.ErrorCode = 5;
                            //order.Error = "Вы уже оплатили данный заказ. Для открытия нового заказа обратитесь к официанту.";
                            order.Error = Helper.GetError(5, language);
                            Helper.saveToLog(0, user_key, "GetOrderDiscount", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + order.TableID + ", ErrorCode: ", "Заказ уже оплачен", 1);
                            return list;
                        }
                        //Проверка закрытия заказа на кассе
                        if (order.OrderStatus != null)
                        {
                            if (order.OrderStatus.StatusID == 2)
                            {
                                order.ErrorCode = 6;
                                //order.Error = "Ваш заказ закрыт. Для открытия нового заказа обратитесь к официанту.";
                                order.Error = Helper.GetError(6, language);
                                Helper.saveToLog(0, user_key, "GetOrderDiscount", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + order.TableID.ToString() + ", StatusID: " + order.OrderStatus.StatusID.ToString(), "Заказ закрыт", 1);
                                return list;
                            }
                        }

                        if (order.ErrorCode != 0)
                        {
                            order.ErrorCode = 100;
                            order.Error = Helper.GetError(100, language);
                            return list;
                        }
                        //Запись заказа в БД
                        OrderData.SqlInsertOrders(restaurantID, phoneNumber, user_key, order, phoneCode);


                        //Скидка veep
                        foreach (var o in list)
                        {
                            o.MainDiscountProc = RestaurantData.GetVeepDiscount(restaurantID); ;
                            o.MainDiscountSum = o.OrderPayment.OrderSum * o.MainDiscountProc / 100;
                            o.OrderPayment.OrderSum = o.OrderPayment.OrderSum - o.MainDiscountSum;
                        }
                        
                    }
                    XMLGenerator<List<Order>> listXML = new XMLGenerator<List<Order>>(list);
                    Helper.saveToLog(0, user_key, "GetOrderDiscount - пришло из модуля", "restaurantID=" + restaurantID.ToString() + ", orderNumber=" + orderNumber, "Найдены заказы: " + listXML.GetStringXML(), 0);
                    return list;
                }
                else
                {
                    return null;
                }
            }
            Helper.saveToLog(0, user_key, "GetOrderDiscount", "restaurantID=" + restaurantID.ToString() + ", orderNumber=" + orderNumber, "Заказ не найден.", 1);
            return null;
        }

        public List<Order> GetOrder(int restaurantID, string orderNumber, string user_key, string phoneCode = "7", int language = 0)
        {
            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "")
            {

                //Запрос к Интеграционному модулю
                string endpointName = "";
                string address = "";

                endpointName = Configs.GetEndpoint(restaurantID);
                address = Configs.GetAddress(restaurantID);

                IntegrationCMD.IntegrationCMDClient cmd = new IntegrationCMD.IntegrationCMDClient(endpointName, address);
                //IntegrationCMD.Order[] orders = cmd.GetOrder(restaurantID, orderNumber, card.CardNumber);

                Helper.saveToLog(0, user_key, "GetOrders - отправлено", "restaurant_id: " + restaurantID.ToString() + ", orderNumber=" + orderNumber, "", 0);
                IntegrationCMD.Order[] orders = cmd.GetOrder(restaurantID, orderNumber, null);
                if (orders != null)
                {
                    List<Order> list = new List<Order>();
                    foreach (var item in orders)
                    {
                        Order order = new Order();
                        order.OrderNumber = item.OrderNumber;
                        order.RestaurantID = item.RestaurantID;
                        order.TableID = item.TableID;
                        if (item.Waiter != null)
                        {
                            Waiter waiter = new Waiter();
                            waiter.ID = item.Waiter.ID;
                            waiter.Name = item.Waiter.Name;
                            order.Waiter = waiter;
                        }
                        if (item.Items != null)
                        {
                            List<OrderItem> list_items = new List<OrderItem>();
                            foreach (var order_item in item.Items)
                            {
                                OrderItem list_item = new OrderItem();
                                list_item.Name = order_item.Name;
                                list_item.Qty = order_item.Qty;
                                list_item.Price = order_item.Price;
                                list_items.Add(list_item);
                            }
                            order.OrderItems = list_items;
                        }
                        if (item.OrderStatus != null)
                        {
                            OrderStatus os = new OrderStatus();
                            os.StatusID = item.OrderStatus.StatusID;
                            os.StatusDate = item.OrderStatus.StatusDate;
                            order.OrderStatus = os;
                        }
                        if (item.OrderPayment != null)
                        {
                            OrderPayment op = new OrderPayment();
                            op.OrderTotal = item.OrderPayment.OrderTotal;
                            op.DiscountSum = item.OrderPayment.DiscountSum;
                            op.OrderSum = item.OrderPayment.OrderSum;
                            op.OrderBank = 0;
                            order.OrderPayment = op;
                        }

                        //Скидка по дисконтной карте (на всякий случай)
                        if (item.DiscountCard != null)
                        {
                            DiscountCard discard = new DiscountCard();
                            discard.CardNumber = (long?)item.DiscountCard.CardNumber;
                            discard.CardStatus = item.DiscountCard.CardStatus;
                            discard.LastDate = item.DiscountCard.LastDate;
                            order.DiscountCard = discard;
                        }


                        order.Message = item.Message;
                        order.Error = item.Error;
                        order.ErrorCode = item.ErrorCode;
                        list.Add(order);

                        Helper.saveToLog(0, user_key, "GetOrders", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + order.TableID + ", ErrorCode: " + item.ErrorCode.ToString(), "", 1);

                        //Проверка ошибки 31 - официант работает с заказом
                        if (item.ErrorCode == 31)
                        {
                            order.ErrorCode = 31;
                            //order.Error = "Официант редактирует заказ. Повторите запрос через минуту.";
                            order.Error = Helper.GetError(31, language);
                            Helper.saveToLog(0, user_key, "GetOrders", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + order.TableID + ", ErrorCode: " + item.ErrorCode.ToString(), "Официант работает с заказом", 1);
                            return list;
                        }
                        //Проверка оплаты заказа в БД
                        if (OrderData.GetStatus(order.OrderNumber) == 2)
                        {
                            order.ErrorCode = 5;
                            //order.Error = "Вы уже оплатили данный заказ. Для открытия нового заказа обратитесь к официанту.";
                            order.Error = Helper.GetError(5, language);
                            Helper.saveToLog(0, user_key, "GetOrders", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + order.TableID + ", ErrorCode: ", "Заказ уже оплачен", 1);
                            return list;
                        }
                        //Проверка закрытия заказа на кассе
                        if (order.OrderStatus != null)
                        {
                            if (order.OrderStatus.StatusID == 2)
                            {
                                order.ErrorCode = 6;
                                //order.Error = "Ваш заказ закрыт. Для открытия нового заказа обратитесь к официанту.";
                                order.Error = Helper.GetError(6, language);
                                Helper.saveToLog(0, user_key, "GetOrders", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + order.TableID.ToString() + ", StatusID: " + order.OrderStatus.StatusID.ToString(), "Заказ закрыт", 1);
                                return list;
                            }
                        }

                        if (order.ErrorCode != 0)
                        {
                            order.ErrorCode = 100;
                            order.Error = Helper.GetError(100, language);
                            return list;
                        }
                        //Запись заказа в БД
                        OrderData.SqlInsertOrders(restaurantID, phoneNumber, user_key, order, phoneCode);


                        //Скидка veep
                        foreach (var o in list)
                        {
                            o.MainDiscountProc = RestaurantData.GetVeepDiscount(restaurantID); ;
                            o.MainDiscountSum = o.OrderPayment.OrderSum * o.MainDiscountProc / 100;
                            o.OrderPayment.OrderSum = o.OrderPayment.OrderSum - o.MainDiscountSum;
                        }

                    }
                    XMLGenerator<List<Order>> listXML = new XMLGenerator<List<Order>>(list);
                    Helper.saveToLog(0, user_key, "GetOrder", "restaurantID=" + restaurantID.ToString() + ", orderNumber=" + orderNumber, "Найдены заказы: " + listXML.GetStringXML(), 0);
                    return list;
                }
                else
                {
                    return null;
                }
            }
            Helper.saveToLog(0, user_key, "GetOrder", "restaurantID=" + restaurantID.ToString() + ", orderNumber=" + orderNumber, "Заказ не найден.", 1);
            return null;
        }

        //Вызов официанта
        public int CallWaiter(int restaurantID, string tableID, string orderNumber, int code, string user_key, string phoneCode = "7", int language = 0)
        {
            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "")
                {
                    try
                    {
                        
                        //Получаем заказ по номеру
                        List<Order> list = new List<Order>();
                        list = GetOrderDiscount(restaurantID, orderNumber, user_key, phoneCode, language, null);
                        //Проверяем номер стола
                        if (list != null)
                        {
                            Order order = list[0];
                            tableID = order.TableID;
                        }
                        
                        //Запрос к Интеграционному модулю
                        string endpointName = "";
                        string address = "";

                        endpointName = Configs.GetEndpoint(restaurantID);
                        address = Configs.GetAddress(restaurantID);
                        /*
                        switch (restaurantID)
                        {
                            case 730410002: /*test
                                endpointName = "BasicHttpBinding_IIntegrationCMD";
                                address = "http://95.84.168.113:9090/";
                                break;
                            case 202930001: /*Luce
                                endpointName = "BasicHttpBinding_IIntegrationCMD";
                                address = "http://92.38.32.63:9090/";
                                break;
                            case 209631111: /*Vogue
                                endpointName = "BasicHttpBinding_IIntegrationCMD1";
                                address = "http://185.26.193.5:9090/";
                                break;
                            case 880540002: /*Хлеб и вино - Улица 1905 года
                                endpointName = "BasicHttpBinding_IIntegrationCMD2";
                                address = "http://95.84.146.191:9090/";
                                break;
                        }
                */

                        IntegrationCMD.IntegrationCMDClient cmd = new IntegrationCMD.IntegrationCMDClient(endpointName, address);
                        int call = cmd.CallWaiter(restaurantID, tableID, orderNumber, code);

                        //Запись сообщения в Messages
                        string message_text = "";
                        switch (code)
                        {
                            case 1:
                                message_text = "Вызов официанта";
                                break;
                            case 2:
                                message_text = "Принести счет";
                                break;
                            case 3:
                                message_text = "Отмена счета";
                                break;
                            //case 8:
                            //    message_text = "Заказ оплачен";
                            //    break;
                        }
                        Messages msg = new Messages { RestaurantID = restaurantID, TableID = tableID, MessageType = code, MessageText = message_text};
                        if (code != 8)
                        {
                            int save_msg = PersonalData.SqlInsertMessage(msg);

                            Helper.saveToMessagesLog(0, "CallWaiter", "restaurantID=" + restaurantID + ", tableID=" + tableID + ", orderNumber=" + orderNumber + ", code=" + code.ToString(), "Отправка сообщения: code=" + code.ToString(), 0);
                        }
                        return call;
                    }
                    catch (Exception ex)
                    {
                        Helper.saveToMessagesLog(0, "CallWaiter", "restaurantID=" + restaurantID + ", tableID=" + tableID + ", orderNumber=" + orderNumber + ", code=" + code.ToString(), "Ошибка при отправке сообщения: " + ex.Message, 1);
                    }
                }
            Helper.saveToMessagesLog(0, "CallWaiter", "restaurantID=" + restaurantID + ", tableID=" + tableID + ", orderNumber=" + orderNumber + ", code=" + code.ToString(), "Ошибка при отправке сообщения: code=" + code.ToString(), 1);
                return 0;
        }

        //Вызов админастратора
        public int CallAdmin(int restaurantID, string tableID, string orderNumber, int code, string user_key, string phoneCode = "7", int language = 0)
        {
            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "")
            {
                try
                {
                    //Получаем заказ по номеру
                    List<Order> list = new List<Order>();
                    list = GetOrderDiscount(restaurantID, orderNumber, user_key, phoneCode, language,null);
                    //Проверяем номер стола
                    if (list != null)
                    {
                        Order order = list[0];
                        tableID = order.TableID;
                    }

                    //Запрос к Интеграционному модулю
                    string endpointName = "";
                    string address = "";

                    endpointName = Configs.GetEndpoint(restaurantID);
                    address = Configs.GetAddress(restaurantID);
                    /*
                    switch (restaurantID)
                    {
                        case 730410002: /*test
                            endpointName = "BasicHttpBinding_IIntegrationCMD";
                            address = "http://95.84.168.113:9090/";
                            break;
                        case 202930001: /*Luce
                            endpointName = "BasicHttpBinding_IIntegrationCMD";
                            address = "http://92.38.32.63:9090/";
                            break;
                        case 209631111: /*Vogue
                            endpointName = "BasicHttpBinding_IIntegrationCMD1";
                            address = "http://185.26.193.5:9090/";
                            break;
                        case 880540002: /*Хлеб и вино - Улица 1905 года
                            endpointName = "BasicHttpBinding_IIntegrationCMD2";
                            address = "http://95.84.146.191:9090/";
                            break;
                    }
            */

                    IntegrationCMD.IntegrationCMDClient cmd = new IntegrationCMD.IntegrationCMDClient(endpointName, address);
                    int call = cmd.CallAdmin(restaurantID, tableID, orderNumber, code);

                    //Module mod = new Module();
                    //int call = mod.CallWaiter(restaurantID, tableID, orderNumber, code);

                    Helper.saveToMessagesLog(0, "CallAdmin", "restaurantID=" + restaurantID + ", tableID=" + tableID + ", orderNumber=" + orderNumber + ", code=" + code.ToString(), "Отправка сообщения: code=" + code.ToString(), 0);
                    return call;
                }
                catch (Exception ex)
                {
                    Helper.saveToMessagesLog(0, "CallAdmin", "restaurantID=" + restaurantID + ", tableID=" + tableID + ", orderNumber=" + orderNumber + ", code=" + code.ToString(), "Ошибка при отправке сообщения: " + ex.Message, 1);
                }
            }
            Helper.saveToMessagesLog(0, "CallAdmin", "restaurantID=" + restaurantID + ", tableID=" + tableID + ", orderNumber=" + orderNumber + ", code=" + code.ToString(), "Ошибка при отправке сообщения: code=" + code.ToString(), 1);
            return 0;
        }

        //Начало оплаты заказа - назначение статуса заказа precheck
        public List<Order> OrderPrecheck(int restaurantID, string orderNumber, decimal paymentSum, Int64? discountCardNumber, string user_key, string phoneCode = "7", int language = 0)
        {
            if (phoneCode == "") { phoneCode = "7"; }
            List<Order> list = new List<Order>();
            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "")
                {
                    //Получаем заказ по номеру
                    list = GetOrderDiscount(restaurantID, orderNumber, user_key, phoneCode, language, discountCardNumber);
                    //Проверка суммы заказа
                    foreach (var item in list)
                    {
                        //paymentSum = 1;
                        if (item.OrderPayment.OrderSum != paymentSum)
                        {
                            list[0].ErrorCode = 20;
                            list[0].Error = "В заказ внесены изменения. Сумма изменена.";
                            Helper.saveToLog(0, user_key, "OrderPrecheck", "restaurantID=" + restaurantID + ", orderNumber=" + orderNumber + ", discountCardNumber=" + discountCardNumber, "В заказ внесены изменения. Сумма изменена. Заказ не записан в БД, пречек не сделан", 0);
                            return list;
                        }
                        else
                        {
                            /* //Статус заказа 2 - заказ закрыт
                            if (item.OrderStatus.StatusID == 2)
                            {
                                // Запись в лог
                                XMLGenerator<List<Order>> listXML = new XMLGenerator<List<Order>>(list);
                                Helper.saveToLog(0, user_key, "OrderPrecheck", "restaurantID=" + restaurantID + ", orderNumber=" + orderNumber + ", discountCardNumber=" + discountCardNumber, "Заказ закрыт: " + listXML.GetStringXML(), 0);
                                return list;
                            }*/
                            // Из-за падения кассы при пречеке принудительно проставляем статус 1 - пречек
                            item.OrderStatus.StatusID = 1;
                            item.OrderStatus.StatusDate = DateTime.Now;
                            item.OrderStatus.StatusName = "Precheck";

                            //Запись заказа в БД
                            OrderData.SqlInsertOrders(restaurantID, phoneNumber, user_key, item);

                            //Запись сообщения об начале оплаты
                            Messages msg = new Messages { RestaurantID = restaurantID, TableID = item.TableID, MessageType = 102, MessageText = "Начало оплаты (пречек)" };
                            int save_msg = PersonalData.SqlInsertMessage(msg);
                        }
                    }

                    // Запись в лог
                    XMLGenerator<List<Order>> listXML2 = new XMLGenerator<List<Order>>(list);
                    Helper.saveToLog(0, user_key, "OrderPrecheck", "restaurantID=" + restaurantID + ", orderNumber=" + orderNumber + ", discountCardNumber=" + discountCardNumber, "Пречек заказа: " + listXML2.GetStringXML(), 0);

                    return list;

                // Из-за падения кассы при пречеке убираем запрос OrderPrecheck
                    /*
                        List<Order> list_new = new List<Order>();
                        //Запрос к Интеграционному модулю                
                        IntegrationCMD.IntegrationCMDClient cmd = new IntegrationCMD.IntegrationCMDClient();
                        //IntegrationCMD.Order[] orders = cmd.CancelPrecheck(restaurantID, orderNumber, null); //Отмена случайного пречека
                        IntegrationCMD.Order[] orders = cmd.OrderPrecheck(restaurantID, orderNumber, discountCardNumber);
                    

                    if (orders != null)
                    {
                        foreach (var item in orders)
                        {
                            Order order = new Order();
                            order.OrderNumber = item.OrderNumber;
                            order.RestaurantID = item.RestaurantID;
                            order.TableID = item.TableID;
                            if (item.Waiter != null)
                            {
                                Waiter waiter = new Waiter();
                                waiter.ID = item.Waiter.ID;
                                waiter.Name = item.Waiter.Name;
                                order.Waiter = waiter;
                            }
                            if (item.Items != null)
                            {
                                List<OrderItem> list_items = new List<OrderItem>();
                                foreach (var order_item in item.Items)
                                {
                                    OrderItem list_item = new OrderItem();
                                    list_item.Name = order_item.Name;
                                    list_item.Qty = order_item.Qty;
                                    list_item.Price = order_item.Price;
                                    list_items.Add(list_item);
                                }
                                order.OrderItems = list_items;
                            }
                            if (item.OrderStatus != null)
                            {
                                OrderStatus os = new OrderStatus();
                                //os.StatusID = item.OrderStatus.StatusID;
                                os.StatusID = 1;
                                os.StatusDate = item.OrderStatus.StatusDate;
                                order.OrderStatus = os;
                            }
                            //order.ErrorCode = item.ErrorCode;
                            order.Error = item.Error;
                            if (item.OrderPayment != null)
                            {
                                OrderPayment op = new OrderPayment();
                                op.OrderTotal = item.OrderPayment.OrderTotal;
                                op.DiscountSum = item.OrderPayment.DiscountSum;
                                op.OrderSum = item.OrderPayment.OrderSum;
                                op.OrderBank = 0; 
                                if (item.OrderPayment.OrderSum != paymentSum)
                                {
                                    order.ErrorCode = 20;
                                    order.Error = "В заказ внесены изменения. Сумма изменена.";
                                    OrderStatus os = new OrderStatus();
                                    os.StatusID = 0;
                                    os.StatusDate = item.OrderStatus.StatusDate;
                                    order.OrderStatus = os;
                                }
                                order.OrderPayment = op;
                            }
                            order.Message = item.Message;


                            list_new.Add(order);
                            //Запись заказа в БД
                            OrderData.SqlInsertOrders(restaurantID, phoneNumber, user_key, order);
                           


                            //Запись сообщения об начале оплаты
                            Messages msg = new Messages { RestaurantID = restaurantID, TableID = order.TableID, MessageType = 102, MessageText = "Начало оплаты (пречек)" };
                            int save_msg = PersonalData.SqlInsertMessage(msg);
                        }
                        XMLGenerator<List<Order>> listXML = new XMLGenerator<List<Order>>(list);
                        Helper.saveToLog(0, user_key, "OrderPrecheck", "restaurantID=" + restaurantID + ", orderNumber=" + orderNumber + ", discountCardNumber=" + discountCardNumber, "Пречек заказа: " + listXML.GetStringXML(), 0);

                        return list_new;
                    }
                      */
                }
            Helper.saveToLog(0, user_key, "OrderPrecheck", "restaurantID=" + restaurantID + ", orderNumber=" + orderNumber + ", discountCardNumber=" + discountCardNumber, "Заказ не записан в БД, пречек не сделан", 0);
            return null;
        }

        //Отмена начала оплаты заказа - назначение статуса заказа Открыт
        public List<Order> OrderCancelPrecheck(int restaurantID, string orderNumber, Int64? discountCardNumber, string user_key, string phoneCode = "7", int language = 0)
        {

            List<Order> list = new List<Order>();
            //костыль
            //Получаем заказ по номеру
            list = GetOrderDiscount(restaurantID, orderNumber, user_key, phoneCode, language,null);
            return list;
            //конец костыля


            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "")
                {
                    //Запрос к Интеграционному модулю
                    string endpointName = "";
                    string address = "";

                    endpointName = Configs.GetEndpoint(restaurantID);
                    address = Configs.GetAddress(restaurantID);
                    /*
                    switch (restaurantID)
                    {
                        case 730410002: /*test
                            endpointName = "BasicHttpBinding_IIntegrationCMD";
                            address = "http://95.84.168.113:9090/";
                            break;
                        case 202930001: /*Luce
                            endpointName = "BasicHttpBinding_IIntegrationCMD";
                            address = "http://92.38.32.63:9090/";
                            break;
                        case 209631111: /*Vogue
                            endpointName = "BasicHttpBinding_IIntegrationCMD1";
                            address = "http://185.26.193.5:9090/";
                            break;
                        case 880540002: /*Хлеб и вино - Улица 1905 года
                            endpointName = "BasicHttpBinding_IIntegrationCMD2";
                            address = "http://95.84.146.191:9090/";
                            break;
                    }
            */

                    IntegrationCMD.IntegrationCMDClient cmd = new IntegrationCMD.IntegrationCMDClient(endpointName, address);
                    //номер дисконтной карты - null
                    IntegrationCMD.Order[] orders = cmd.CancelPrecheck(restaurantID, orderNumber, null);
                    if (orders != null)
                    {
                        foreach (var item in orders)
                        {
                            Order order = new Order();
                            order.OrderNumber = item.OrderNumber;
                            order.RestaurantID = item.RestaurantID;
                            order.TableID = item.TableID;
                            if (item.Waiter != null)
                            {
                                Waiter waiter = new Waiter();
                                waiter.ID = item.Waiter.ID;
                                waiter.Name = item.Waiter.Name;
                                order.Waiter = waiter;
                            }
                            if (item.Items != null)
                            {
                                List<OrderItem> list_items = new List<OrderItem>();
                                foreach (var order_item in item.Items)
                                {
                                    OrderItem list_item = new OrderItem();
                                    list_item.Name = order_item.Name;
                                    list_item.Qty = order_item.Qty;
                                    list_item.Price = order_item.Price;
                                    list_items.Add(list_item);
                                }
                                order.OrderItems = list_items;
                            }
                            if (item.OrderStatus != null)
                            {
                                OrderStatus os = new OrderStatus();
                                //os.StatusID = item.OrderStatus.StatusID;
                                os.StatusID = 0;
                                os.StatusDate = item.OrderStatus.StatusDate;
                                order.OrderStatus = os;
                            }
                            if (item.OrderPayment != null)
                            {
                                OrderPayment op = new OrderPayment();
                                op.OrderTotal = item.OrderPayment.OrderTotal;
                                op.DiscountSum = item.OrderPayment.DiscountSum;
                                op.OrderSum = item.OrderPayment.OrderSum;
                                op.OrderBank = 0;
                                order.OrderPayment = op;
                            }
                            order.Message = item.Message;
                            //order.ErrorCode = item.ErrorCode;
                            order.Error = item.Error;
                            list.Add(order);
                            //Запись заказа в БД
                            OrderData.SqlInsertOrders(restaurantID, phoneNumber, user_key, order);
                            XMLGenerator<List<Order>> listXML = new XMLGenerator<List<Order>>(list);
                            Helper.saveToLog(0, user_key, "OrderCancelPrecheck", "restaurantID=" + restaurantID + ", orderNumber=" + orderNumber + ", discountCardNumber=" + discountCardNumber, "Пречек заказа: " + listXML.GetStringXML(), 0);
                        }
                        return list;
                    }
                }
            return null;
        }

        //Оплата заказа
        public List<Order> GetPaymentBinding(int restaurantID, string orderNumber, decimal paymentSum, long paymentBank, string user_key, string bindingId, decimal tippingProcent, string phoneCode = "7", int language = 0)
        {
            List<Order> list = new List<Order>();
            string phoneNumber = CheckUserKey(user_key);

            if (phoneNumber != "")
            {
                if (phoneCode == "") { phoneCode = "7"; }
                string clientId = GetClientId(phoneNumber, phoneCode);
                //Поиск заказа в БД
                string orderNum = OrderData.SqlFindOrders(phoneNumber, user_key, orderNumber, restaurantID);
                if (!String.IsNullOrWhiteSpace(orderNum))
                {
                    //Получаем заказ по номеру
                    list = GetOrder(restaurantID, orderNumber, user_key, phoneCode, language);
                    //Проверка суммы заказа
                    foreach (var item in list)
                    {
                        //для теста
                        // if (item.OrderNumber == "00045C98") { item.ErrorCode = 31; }

                        if (item.ErrorCode == 31)
                        {
                            //item.Error = "Подождите минуту. Официант работает с заказом";
                            item.Error = Helper.GetError(31, language);
                            Helper.saveToLog(0, user_key, "GetPaymentBinding", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + item.TableID + ", ErrorCode: " + item.ErrorCode.ToString(), "Официант работает с заказом", 1);
                            return list;
                        }
                        //проверка оплаты заказа
                        if (OrderData.GetStatus(item.OrderNumber) == 2)
                        {
                            item.ErrorCode = 5;
                            //item.Error = "Вы уже оплатили данный заказ. Для открытия нового заказа обратитесь к официанту.";
                            item.Error = Helper.GetError(5, language);
                            Helper.saveToLog(0, user_key, "GetPaymentBinding", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + item.TableID + ", ErrorCode: " + item.ErrorCode.ToString(), "Заказ уже оплачен", 1);
                            return list;
                        }

                        //Для проверки суммы убираем из нее скидку veep и добавляем скидку в копейках в платеж банку
                        if (item.MainDiscountSum != 0)
                        {
                            item.OrderPayment.OrderSum = item.OrderPayment.OrderSum + item.MainDiscountSum;
                            paymentBank = paymentBank - Convert.ToInt64(item.MainDiscountSum * 100);
                        }

                        if (item.OrderPayment.OrderSum != paymentSum)
                        {
                            item.ErrorCode = 20;
                            //item.Error = "В заказ внесены изменения. Сумма изменена.";
                            item.Error = Helper.GetError(20, language);
                            Helper.saveToLog(0, user_key, "GetPaymentBinding", "restaurant_id: " + restaurantID.ToString() + ", tableID: " + item.TableID + ", paymentSum=" + paymentSum.ToString() + ",item.OrderPayment.OrderSum=" + item.OrderPayment.OrderSum.ToString() + "paymentBank=" + paymentBank.ToString() + ", ErrorCode: " + item.ErrorCode.ToString(), "В заказ внесены изменения. Сумма изменена.", 1);
                            //list = OrderCancelPrecheck(restaurantID, orderNumber, null, user_key);
                            return list;
                        }
                        if (item.ErrorCode != 0)
                        {
                            item.ErrorCode = 100;
                            item.Error = Helper.GetError(100, language);
                            return list;
                        }
                    }

                    //Замена номера заказа для повторной оплаты
                    string new_number = Helper.getNewGUID();
                    int ret = OrderData.SqlChangeNumberOrder(orderNum, new_number);
                    if (ret > 0)
                    {
                        orderNum = new_number;
                    }



                    if (!String.IsNullOrWhiteSpace(bindingId))
                    {
                        //Оплата заказа с использованием связок

                        //Регистрация заказа в платежной системе
                        registerResponse response = new registerResponse();
                        string returnUrl = "http://185.26.113.204/Payment/Result";
                        //string returnUrl = "http://test.veep.su/Payment/Result";
                        response = MerchantData.registerOrder(restaurantID, orderNum, paymentBank, returnUrl, clientId, bindingId);
                        if (response != null)
                        {
                            DateTime paymentDate = DateTime.Now;
                            XMLGenerator<registerResponse> respon = new XMLGenerator<registerResponse>(response);
                            Helper.saveToLog(0, user_key, "GetPaymentBinding/registerOrder", "restaurantID=" + restaurantID.ToString() + ", phoneNumber=" + phoneNumber + ", orderNumber=" + orderNum + ", paymentSum=" + paymentSum.ToString() + ", paymentBank=" + paymentBank.ToString() + ", clientId=" + clientId + ", bindingId=" + bindingId, "Регистрация заказа в платежной системе: orderId=" + response.orderId + ", formUrl=" + response.formUrl + ", errorCode=" + response.errorCode.ToString() + ", errorMessage=" + response.errorMessage + ", response = " + respon.GetStringXML(), 0);
                            //Вставка платежа в БД
                            string insert_result = OrderData.SqlInsertPayment(user_key, restaurantID, phoneNumber, orderNumber, orderNum, paymentBank, DateTime.Now, response.orderId, response.formUrl, response.errorCode, response.errorMessage, clientId, bindingId, tippingProcent, phoneCode);

                            paymentOrderBindingResponse paymentResponse = new paymentOrderBindingResponse();
                            paymentResponse = MerchantData.paymentOrderBinding(restaurantID, response.orderId, bindingId);
                            if (paymentResponse != null)
                            {
                                if (paymentResponse.redirect != null && paymentResponse.redirect != "")
                                {
                                    list[0].FormUrl = paymentResponse.redirect;
                                }
                                else
                                {
                                    list[0].FormUrl = paymentResponse.acsUrl + "&MD=" + response.orderId + "&PaReq=" + paymentResponse.pareq + "&TermUrl=" + paymentResponse.termUrl;
                                }
                            }

                            if (tippingProcent > 0)
                            {
                                //расчет чаевых в рубл с округлением
                                //decimal tippingSum = Decimal.Round((tippingProcent * paymentSum) / 100);

                                OrderData.SqlInsertTipping(restaurantID, orderNum, tippingProcent, phoneNumber, phoneCode);
                            }

                            return list;
                        }
                    }
                    else
                    {
                        //Оплата заказа без использования связок
                        //Регистрация заказа в платежной системе
                        registerResponse response = new registerResponse();
                        string returnUrl = "http://185.26.113.204/Payment/Result";
                        //string returnUrl = "http://test.veep.su/Payment/Result";
                        response = MerchantData.registerOrder(restaurantID, orderNum, paymentBank, returnUrl, clientId, "");
                        if (response != null)
                        {
                            DateTime paymentDate = DateTime.Now;
                            Helper.saveToLog(0, user_key, "registerOrder", "restaurantID=" + restaurantID.ToString() + ", phoneNumber=" + phoneNumber + ", orderNumber=" + orderNum + ", paymentSum=" + paymentSum.ToString() + ", paymentBank=" + paymentBank.ToString() + ", clientId=" + clientId.ToString(), "Регистрация заказа в платежной системе: orderId=" + response.orderId + ", formUrl=" + response.formUrl + ", errorCode=" + response.errorCode.ToString() + ", errorMessage=" + response.errorMessage, 0);
                            //Вставка платежа в БД
                            string insert_result = OrderData.SqlInsertPayment(user_key, restaurantID, phoneNumber, orderNumber, orderNum, paymentBank, DateTime.Now, response.orderId, response.formUrl, response.errorCode, response.errorMessage, clientId, null, 0);
                            if (!String.IsNullOrWhiteSpace(response.formUrl))
                            {
                                list[0].FormUrl = response.formUrl;
                            }
                            else
                            {
                                list[0].Error = response.errorMessage;
                            }

                            //return (!String.IsNullOrWhiteSpace(response.formUrl)) ? response.formUrl : response.errorMessage;
                            return list;
                        }
                    }
                }
                else
                {
                    if (list == null || list.Count == 0)
                    {
                        return new List<Order>(){
                           new Order(){
                               //ErrorCode = 1,
                               //Error = "Заказ не найден или оплачен. Для открытия нового заказа обратитесь к официанту."}
                               ErrorCode = 100,
                               Error = Helper.GetError(100,language)}
                       };
                    }
                    else
                    {
                        //list[0].Error = "Заказ не найден или оплачен. Для открытия нового заказа обратитесь к официанту.";
                        list[0].ErrorCode = 100;
                        list[0].Error = Helper.GetError(100, language);
                        return list;
                    }
                }
            }

            return null;
        }

        //Оплата заказа без связок
        public string GetPayment(int restaurantID, string orderNumber, decimal paymentSum, long paymentBank, string user_key, int language = 0)
        {
            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "")
            {
                string clientId = GetClientId(phoneNumber);
                //Поиск заказа в БД
                string orderNum = OrderData.SqlFindOrders(phoneNumber, user_key, orderNumber, restaurantID);
                if (!String.IsNullOrWhiteSpace(orderNum))
                {
                    //Оплата заказа без использования связок
                    //Регистрация заказа в платежной системе
                    registerResponse response = new registerResponse();
                    //string returnUrl = "http://localhost:49935/Payment/Result";
                    string returnUrl = "http://185.26.113.204/Payment/Result";
                    //string returnUrl = "http://test.veep.su/Payment/Result";
                    response = MerchantData.registerOrder(restaurantID, orderNum, paymentBank, returnUrl, clientId, "");
                    if (response != null)
                    {
                        DateTime paymentDate = DateTime.Now;
                        Helper.saveToLog(0, user_key, "GetPayment", "restaurantID=" + restaurantID.ToString() + ", phoneNumber=" + phoneNumber + ", orderNumber=" + orderNum + ", paymentSum=" + paymentSum.ToString() + ", paymentBank=" + paymentBank.ToString() + ", clientId=" + clientId.ToString(), "Регистрация заказа в платежной системе: orderId=" + response.orderId + ", formUrl=" + response.formUrl + ", errorCode=" + response.errorCode.ToString() + ", errorMessage=" + response.errorMessage, 0);
                        //Вставка платежа в БД
                        string insert_result = OrderData.SqlInsertPayment(user_key, restaurantID, phoneNumber, orderNumber, orderNum, paymentBank, DateTime.Now, response.orderId, response.formUrl, response.errorCode, response.errorMessage, clientId, null, 0);
                        return (!String.IsNullOrWhiteSpace(response.formUrl)) ? response.formUrl : response.errorMessage;
                    }
                }
                else
                {
                    return "Заказ в БД не найден";
                }
            }

            return null;
        }



        //Проверка статуса заказа
        public string GetOrderStatus(string orderId, string user_key, int language = 0)
        {
            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "")
            {
                orderStatusResponse response = new orderStatusResponse();
                response = MerchantData.getOrderStatus(orderId, 2);
                if (response != null)
                {
                    return response.OrderStatus.ToString();
                }
            }
            return null;
        }

        //Отправка сообщения
        public int SendMessage(int restaurantID, string tableID, int messageType, string messageText, int errorCode, string errorText, int language = 0)
        {
            int result = 0;
            if(restaurantID > 0)
            {
                Messages msg = new Messages { RestaurantID = restaurantID, TableID = tableID, MessageType = messageType, MessageText = messageText, ErrorCode = errorCode, ErrorText = errorText };
                result = PersonalData.SqlInsertMessage(msg);
            }
            return result;
        }


        //Оценка качества обслуживания в ресторане
        public int SaveRating(int restaurantID, string orderNumber, int rating, string user_key)
        {
            int ret = 0;
            //Запись в БД результат оценки качества
            string phoneNumber = CheckUserKey(user_key);
            if (phoneNumber != "")
            {
                ret = OrderData.SaveRating(restaurantID, orderNumber, rating, user_key);
            }
            return ret;
        }

        #endregion

        #region Restaurant

        //Список ресторанов, подключенных к сети
        public List<RestNetwork> FindRestaurant(string user_key, int language = 0)
        {
            //language = 1;
            List<RestNetwork> list = new List<RestNetwork>();
            if (CheckUserKey(user_key) != "")
            {
                list = RestaurantData.GetRestNetwork(user_key, language);
            }
            return list;
        }

        //Информация о ресторане
        public Restaurant RestaurantInfo(int restaurantID, string user_key, int language = 0)
        {
            Restaurant restaurant = new Restaurant();
            if (CheckUserKey(user_key) != "")
            {
                restaurant = RestaurantData.RestaurantInfo(restaurantID, user_key, language);
            }
            return restaurant;
        }

        //Список ресторанов для селекта
        public List<Restaurant> RestaurantList()
        {
            List<Restaurant> list = RestaurantData.GetRestaurants();
            return list;
        }

    #endregion

        #region Personal

        //Авторизация
        public Personal PersonalLogin(string login, string psw)
        {
            login = login.Trim().Replace("'", "").Replace(" ", "");
            psw = psw.Trim().Replace("'", "").Replace(" ", "");
            Personal personal = new Personal();
            personal = PersonalData.SqlLogin(login, psw);

            return personal;
        }

        //Напоминание пароля 
        public int PswRemember(string login)
        {
            int result = 0;
            login = login.Trim().Replace("'", "").Replace(" ", "");
            Personal personal = new Personal();
            personal = PersonalData.SqlLogin(login, null);
            if (personal == null)
            {
                result = 1; //Неверный логин/пароль
            }
            else
            {
                if (personal.RolesID != 1)
                {
                    result = 2; //Роль пользователя "официант"
                }
                else
                {
                    try
                    {
                        if (!String.IsNullOrWhiteSpace(personal.PhoneNumber))
                        {
                            //Роль пользователя "администратор" - отправляем смс с паролем
                            SmsServiceSoapClient comm = new SmsServiceSoapClient();
                            string sessionID = comm.GetSessionID("novatorov", "Paygo2015");
                            Message msg = new Message();
                            msg.Data = DateTime.Now.ToString();
                            msg.SourceAddress = "VEEP";
                            msg.DestinationAddresses = new string[] { personal.PhoneNumber };
                            msg.Data = "Ваш пароль: " + personal.Psw;
                            msg.Validity = 10;
                            string[] sms_send = comm.SendMessage(sessionID, msg);
                            Helper.saveToLog(0, "", "PswRemember", "login: " + login, "Пароль отправлен на номер администратора " + personal.PhoneNumber, 0);
                        }
                        else
                        {
                            result = 3;
                            Helper.saveToLog(0, "", "PswRemember", "login: " + login, "Неправильный номер телефона администратора: " + personal.PhoneNumber, 1);
                        }

                    }
                    catch (Exception e)
                    {
                        Helper.saveToLog(0, "", "PswRemember", "login: " + login, "Внутренняя ошибка сервиса: " + e.Message, 1);
                        result = 3;
                    }
                }
            }
            return result;
        }

        //Получение профиля
        public Profile GetProfile(int restaurantID)
        {
            Profile profile = new Profile();
            if (restaurantID != 0)
            {
                profile = PersonalData.SqlProfile(restaurantID);
            }
            return profile;
        }

        //Смена пароля администратора/официанта
        public int ChangePersonalPsw(int personalID, string newPsw)
        {
            int result = 0;
            if (personalID != 0 && !String.IsNullOrWhiteSpace(newPsw))
            {
                result = PersonalData.SqlChangePersonalPsw(personalID, newPsw);
                return result;
            }
            return result;
        }

        //Получение списка сообщений
        public List<Messages> GetMessages(int restaurantID, int mode)
        {
            //int personalID = 1;
            List<Messages> list = PersonalData.SqlGetMessages(restaurantID, mode);

            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        //Получение списка сообщений
        public List<Messages> GetMessagesPersonal(int restaurantID, int mode, int personalID)
        {
            //int personalID = 1;
            List<Messages> list = PersonalData.SqlGetMessages(restaurantID, mode, personalID);

            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        //Закрытие сообщения - прооставление статуса прочитано
        public int ReadMessage(int messageID, int personalID)
        {
            int result = 0;
            result = PersonalData.SqlIsRead(messageID, personalID, 1);

            return result;
        }

        //Получение истории платежей
        public List<HistoryPayments> GetHistoryPayments(int restaurantID)
        {
            List<HistoryPayments> list = PersonalData.SqlGetHistoryPayments(restaurantID);

            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        //Получение истории платежей
        public List<HistoryPayments> GetHistoryPaymentsPersonal(int restaurantID, int personalID)
        {
            List<HistoryPayments> list = PersonalData.SqlGetHistoryPayments(restaurantID, personalID);

            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        #endregion


    }
}
