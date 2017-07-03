using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using HttpUtils;
using RestService.Models;
using RestService.BLL;

namespace RestService
{
    public class MerchantData
    {
        //Получение списка возможных связок
        public static getBindingsResponse getBindings(string clientId)
        {
            string endPoint = @"https://" + Configs.MerchantHost + "getBindings.do?";
            //string endPoint = @"https://3dsec.sberbank.ru/payment/rest/getBindings.do?";
            //string endPoint = @"https://securepayments.sberbank.ru/payment/rest/getBindings.do?";
            var client = new RestClient(endPoint);
            string param = "userName=" + Configs.MerchantUser + "&password=" + Configs.MerchantPsw + "&clientId=" + clientId;
            //string param = "userName=berlinetz-api&password=berlinetz&clientId=" + clientId;
            //string param = "userName=berlinetz-api&password=belnet00&clientId=" + clientId;
            var json = client.MakeRequest(param);

            if (json != null)
            {
                getBindingsResponse Bindings = JsonConvert.DeserializeObject<getBindingsResponse>(json);
                Helper.saveToLog(0, "", "MerchantData.getBindings", "param=" + param, "Получение списка связок: errorCode=" + Bindings.errorCode.ToString() + ", errorMessage=" + Bindings.errorMessage + ", json=" + json, 0);
                return Bindings;
            }
            else
            {
                Helper.saveToLog(0, "", "MerchantData.getBindings", "param=" + param, "Ошибка при получении списка связок:  json=" + json, 1);
            }

            return null;
        }

        //Деактивация связки
        public static messageResponse unbindCard(string bindingId)
        {
            string endPoint = @"https://" + Configs.MerchantHost + "unBindCard.do?";
            //string endPoint = @"https://3dsec.sberbank.ru/payment/rest/unBindCard.do?";
            //string endPoint = @"https://securepayments.sberbank.ru/payment/rest/unBindCard.do?";
            var client = new RestClient(endPoint);
            string param = "userName=" + Configs.MerchantUser + "&password=" + Configs.MerchantPsw + "&bindingId=" + bindingId;
            //string param = "userName=berlinetz-api&password=berlinetz&bindingId=" + bindingId;
            //string param = "userName=berlinetz-api&password=belnet00&bindingId=" + bindingId;
            var json = client.MakeRequest(param);

            if (json != null)
            {
                messageResponse response = JsonConvert.DeserializeObject<messageResponse>(json);
                Helper.saveToLog(0, "", "MerchantData.unbindCard", "param=" + param, "Деактивация связки: errorCode=" + response.errorCode.ToString() + ", errorMessage=" + response.errorMessage + ", json=" + json, 0);
                return response;
            }
            else
            {
                Helper.saveToLog(0, "", "MerchantData.unbindCard", "param=" + param, "Ошибка при деактивации связки:  json=" + json, 1);
            }

            return null;
        }

        //Активация связки
        public static messageResponse bindCard(string bindingId)
        {
            string endPoint = @"https://" + Configs.MerchantHost + "bindCard.do?";
            //string endPoint = @"https://3dsec.sberbank.ru/payment/rest/bindCard.do?";
            //string endPoint = @"https://securepayments.sberbank.ru/payment/rest/bindCard.do?";
            var client = new RestClient(endPoint);
            string param = "userName=" + Configs.MerchantUser + "&password=" + Configs.MerchantPsw + "&bindingId=" + bindingId;
            //string param = "userName=berlinetz-api&password=berlinetz&bindingId=" + bindingId;
            //string param = "userName=berlinetz-api&password=belnet00&bindingId=" + bindingId;
            var json = client.MakeRequest(param);

            if (json != null)
            {
                messageResponse response = JsonConvert.DeserializeObject<messageResponse>(json);
                Helper.saveToLog(0, "", "MerchantData.bindCard", "param=" + param, "Активация связки: errorCode=" + response.errorCode.ToString() + ", errorMessage=" + response.errorMessage + ", json=" + json, 0);
                return response;
            }
            else
            {
                Helper.saveToLog(0, "", "MerchantData.bindCard", "param=" + param, "Ошибка при активации связки:  json=" + json, 1);
            }

            return null;
        }

        //Регистрация заказа в платежной системе
        public static registerResponse registerOrder(int restaurantID, string orderNumber, long amount, string returnUrl, string clientId, string bindingId)
        {
            string endPoint = @"https://" + Configs.MerchantHost + "register.do?";
            //string endPoint = @"https://3dsec.sberbank.ru/payment/rest/register.do?";
            //string endPoint = @"https://securepayments.sberbank.ru/payment/rest/register.do?";
            var client = new RestClient(endPoint);

            string user_name = "";
            string psw = "";

            user_name = Configs.GetMerchantUser(restaurantID);
            psw = Configs.GetMerchantPsw(restaurantID);
            /*
            switch (restaurantID)
            {
                case 730410002: /*test
                    user_name = Configs.MerchantUser2;
                    psw = Configs.MerchantPsw2;
                    break;
                case 202930001: /*Luce
                    user_name = Configs.MerchantUser2;
                    psw = Configs.MerchantPsw2;
                    break;
                case 209631111: /*Vogue
                    user_name = Configs.MerchantUser3;
                    psw = Configs.MerchantPsw3;
                    break;
            }
            */

            returnUrl += "?restaurantID=" + restaurantID;
            string param = "amount=" + amount.ToString() + "&orderNumber=" + orderNumber + "&password=" + psw + "&returnUrl=" + returnUrl + "&userName=" + user_name + "&pageView=MOBILE&clientId=" + clientId;
            param += "&bindingId=" + bindingId;
            //string param = "amount=" + amount.ToString() + "&orderNumber=" + orderNumber + "&password=berlinetz&returnUrl=" + returnUrl + "&userName=berlinetz-api&pageView=MOBILE&clientId=" + clientId;
            //string param = "amount=" + amount.ToString() + "&orderNumber=" + orderNumber + "&password=belnet00&returnUrl=" + returnUrl + "&userName=berlinetz-api&pageView=MOBILE&clientId=" + clientId;
            var json = client.MakeRequest(param);

            if (json != null)
            {
                registerResponse response = JsonConvert.DeserializeObject<registerResponse>(json);
                Helper.saveToLog(0, "", "MerchantData.registerOrder", "param=" + param, "Регистрация заказа в платежной системе: errorCode=" + response.errorCode.ToString() + ", errorMessage=" + response.errorMessage + ", json=" + json, 0);
                return response;
            }
            else
            {
                Helper.saveToLog(0, "", "MerchantData.registerOrder", "param=" + param, "Ошибка при регистрации заказа в платежной системе:  json=" + json, 1);
            }

            return null;
        }

        //Регистрация двухстадийного платежа в платежной системе
        public static registerResponse registerPreOrder(string orderNumber, long amount, string returnUrl, string clientId, string bindingId)
        {
            string endPoint = @"https://" + Configs.MerchantHost + "registerPreAuth.do?";
            //string endPoint = @"https://3dsec.sberbank.ru/payment/rest/registerPreAuth.do?";
            //string endPoint = @"https://securepayments.sberbank.ru/payment/rest/registerPreAuth.do?";
            var client = new RestClient(endPoint);
            string param = "amount=" + amount.ToString() + "&orderNumber=" + orderNumber + "&password=" + Configs.MerchantPsw + "&returnUrl=" + returnUrl + "&userName=" + Configs.MerchantUser + "&pageView=MOBILE&clientId=" + clientId;
            //string param = "amount=" + amount.ToString() + "&orderNumber=" + orderNumber + "&password=berlinetz&returnUrl=" + returnUrl + "&userName=berlinetz-api&pageView=MOBILE&clientId=" + clientId;
            //string param = "amount=" + amount.ToString() + "&orderNumber=" + orderNumber + "&password=belnet00&returnUrl=" + returnUrl + "&userName=berlinetz-api&pageView=MOBILE&clientId=" + clientId;
            var json = client.MakeRequest(param);

            if (json != null)
            {
                registerResponse response = JsonConvert.DeserializeObject<registerResponse>(json);
                Helper.saveToLog(0, "", "RegisterBinding", "https://" + Configs.MerchantHost + "registerPreAuth.do?" + param, "Привязка карты registerPreOrder: " + json.ToString(), 0);
                return response;
            }

            return null;
        }

        //Проверка статуса заказа
        public static orderStatusResponse getOrderStatus(string orderId, int mode)
        {
            string endPoint = @"https://" + Configs.MerchantHost + "getOrderStatus.do?";
            //string endPoint = @"https://3dsec.sberbank.ru/payment/rest/getOrderStatus.do?";
            //string endPoint = @"https://securepayments.sberbank.ru/payment/rest/getOrderStatus.do?";
            var client = new RestClient(endPoint);
            string param = "";
            string user_name = "";
            string psw = "";
            
            user_name = Configs.GetMerchantUserMode(mode);
            psw = Configs.GetMerchantPswMode(mode);
            /*
            switch (mode)
            {
                case 0: /*Test
                    user_name = Configs.MerchantUser2;
                    psw = Configs.MerchantPsw2;
                    break;
                case 1: /*привязка карты
                    user_name = Configs.MerchantUser;
                    psw = Configs.MerchantPsw;
                    break;
                case 2: /*Luce
                    user_name = Configs.MerchantUser2;
                    psw = Configs.MerchantPsw2;
                    break;
                case 3: /*Vogue
                    user_name = Configs.MerchantUser3;
                    psw = Configs.MerchantPsw3;
                    break;
                case 4: /*Хлеб и вино - Улица 1905 года
                    user_name = Configs.MerchantUser4;
                    psw = Configs.MerchantPsw4;
                    break;
            }
            */
            //if (mode == 1)
            //{
            param = "orderId=" + orderId + "&language=ru&password=" + psw + "&userName=" + user_name;
           // }
           // else
           // {
           //     param = "orderId=" + orderId + "&language=ru&password=" + Configs.MerchantPsw2 + "&userName=" + Configs.MerchantUser2;
           // }
            //string param = "orderId=" + orderId + "&language=ru&password=berlinetz&userName=berlinetz-api";
            //string param = "orderId=" + orderId + "&language=ru&password=belnet00&userName=berlinetz-api";
            var json = client.MakeRequest(param);
            if (json != null)
            {
                orderStatusResponse response = JsonConvert.DeserializeObject<orderStatusResponse>(json);

                return response;
            }
            return null;
        }

        //Отмена двухстадийного заказа
        public static messageResponse reverse(string orderId)
        {
            string endPoint = @"https://" + Configs.MerchantHost + "reverse.do?";
            //string endPoint = @"https://3dsec.sberbank.ru/payment/rest/reverse.do?";
            //string endPoint = @"https://securepayments.sberbank.ru/payment/rest/reverse.do?";
            var client = new RestClient(endPoint);
            string param = "orderId=" + orderId + "&password=" + Configs.MerchantPsw + "&userName=" + Configs.MerchantUser;
            //string param = "orderId=" + orderId + "&password=berlinetz&userName=berlinetz-api";
            //string param = "orderId=" + orderId + "&password=belnet00&userName=berlinetz-api";
            var json = client.MakeRequest(param);
            if (json != null)
            {
                messageResponse response = JsonConvert.DeserializeObject<messageResponse>(json);
                return response;
            }
            return null;
        }

        //Оплата заказа с помощью связок
        public static paymentOrderBindingResponse paymentOrderBinding(int restaurantID, string orderId, string bindingId)
        {
            string endPoint = @"https://" + Configs.MerchantHost + "paymentOrderBinding.do?";
            //string endPoint = @"https://3dsec.sberbank.ru/payment/rest/paymentOrderBinding.do?";
            //string endPoint = @"https://securepayments.sberbank.ru/payment/rest/paymentOrderBinding.do?";

            string user_name = "";
            string psw = "";

            user_name = Configs.GetMerchantUser(restaurantID);
            psw = Configs.GetMerchantPsw(restaurantID);

            /*
            switch (restaurantID)
            {
                case 730410002: /*test
                    user_name = Configs.MerchantUser2;
                    psw = Configs.MerchantPsw2;
                    break;
                case 202930001: /*Luce
                    user_name = Configs.MerchantUser2;
                    psw = Configs.MerchantPsw2;
                    break;
                case 209631111: /*Vogue
                    user_name = Configs.MerchantUser3;
                    psw = Configs.MerchantPsw3;
                    break;
            }
            */

            string param = "mdOrder=" + orderId + "&bindingId=" + bindingId + "&password=" + psw + "&userName=" + user_name;
            //string param = "mdOrder=" + orderId + "&bindingId=" + bindingId + "&password=berlinetz&userName=berlinetz-api";
            //string param = "mdOrder=" + orderId + "&bindingId=" + bindingId + "&password=belnet00&userName=berlinetz-api";
            Helper.saveToLog(0, "", "paymentOrderBinding", "restaurantID=" + restaurantID.ToString() + ", orderId=" + orderId + ", bindingId=" + bindingId, "Оплата заказа по связкам: отправлен запрос=" + endPoint + param, 0);
            var client = new RestClient(endPoint, HttpVerb.POST,param);
            var json = client.MakeRequest(param,0);
            if (json != null)
            {
                paymentOrderBindingResponse response = JsonConvert.DeserializeObject<paymentOrderBindingResponse>(json);
                Helper.saveToLog(0, "", "paymentOrderBinding", "restaurantID=" + restaurantID.ToString() + ", orderId=" + orderId + ", bindingId=" + bindingId, "Оплата заказа по связкам: получен ответ=" + json.ToString(), 0);
                return response;
            }
            else
            {
                Helper.saveToLog(0, "", "paymentOrderBinding", "restaurantID=" + restaurantID.ToString() + ", orderId=" + orderId + ", bindingId=" + bindingId, "Оплата заказа по связкам: получен ответ=" + json.ToString(), 1);
            }
            return null;
        }

        //Регистрация заказа в ACS
        //public static paymentOrderBindingResponse paymentACS(string mdorder, paymentOrderBindingResponse payment)
        public static string paymentACS(string mdorder, paymentOrderBindingResponse payment)
        {
            using (var webClient = new WebClient())
            {
                var pars = new NameValueCollection();
                pars.Add("MD", mdorder);
                pars.Add("PaReq", payment.pareq);
                pars.Add("TermUrl", payment.termUrl);
                var resp = webClient.UploadValues(payment.acsUrl, pars);
                if (resp != null)
                {
                    //paymentOrderBindingResponse response = JsonConvert.DeserializeObject<paymentOrderBindingResponse>(json);
                    var json = Encoding.GetEncoding("utf-8").GetString(resp);
                    if (json != null)
                    {
                        //paymentOrderBindingResponse response = JsonConvert.DeserializeObject<paymentOrderBindingResponse>(json);
                        return json;
                    }
                }
            }
            /*
            string endPoint = payment.acsUrl + "?";
            string param = @"MD=" + mdorder + "&PaReq=" + payment.pareq + "&TermUrl=" + payment.termUrl;
            var client = new RestClient(endPoint, HttpVerb.POST, param, "text/html");
            //var json = client.MakeRequest(param);
            var json = client.ACSRequest(param);
            if (json != null)
            {
                paymentOrderBindingResponse response = JsonConvert.DeserializeObject<paymentOrderBindingResponse>(json);
                return response;
            }
            else
            {
            }
            */
            return null;
        }

    }
}