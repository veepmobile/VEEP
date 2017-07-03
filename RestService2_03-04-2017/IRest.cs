using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RestService.Models;

namespace RestService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IRest
    {
        #region Account

        //Поиск аккаунта по номеру телефона при старте Приложения
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        Account FindAccount(string phoneNumber, string phoneCode = "7", int language = 0);

        //Регистрация аккаунта
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        Account RegistrAccount(string FirstName, string LastName, string PhoneModel, string PhoneNumber, string PhoneUID, int? PhoneOS_ID, string Pswd, string Email, string PhoneCode = "7", int language = 0);

        //Проверка введенного кода SMS
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        bool CheckSMS(string phoneNumber, string SMScode, string phoneCode = "7");

        //Изменение статуса аккаунта
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int UpdateAccountStatus(int accountID, int statusID);

        //Авторизация
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        string Login(string phoneNumber, string psw, string phoneCode = "7");

        //Проверка открытой сессии
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        string CheckSession(string phoneNumber, string phoneCode = "7");

        //Блокировка аккаунта и завершение открытой сессии
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int BlockedAccount(string phoneNumber, int block_id, string phoneCode = "7");

        //Обновление данных аккаунта
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        Account UpdateAccount(string FirstName, string LastName, string PhoneModel, string PhoneNumber, string PhoneUID, int? PhoneOS_ID, string Email, string user_key, string phoneCode = "7", int language = 0);

        //Смена пароля
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int ChangePsw(string phoneNumber, string oldPsw, string newPsw, string user_key, string phoneCode = "7");

        //Выход и завершение сессии
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int Exit(string phoneNumber, string user_key, string phoneCode = "7");



        #endregion

        #region DiscountCard

        //Загрузка привязанных карт в Приложение
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Models.DiscountCard> FindDiscountCard(string phoneNumber, string user_key, string phoneCode = "7");

        //Привязка дисконтной карты в Системе
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int InsertDiscountCard(string phoneNumber, int restaurantID, Int64 cardNumber, int cardStatus, string user_key, string phoneCode = "7");

        //Изменение статуса (удаление) дисконтной карты в Системе
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        Int64 UpdateDiscountCard(string phoneNumber, Int64 cardNumber, int cardStatus, string user_key, string phoneCode = "7");

        #endregion

        #region Bindings

        //Запрос списка возможных связок
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Binding> GetBindings(string phoneNumber, string user_key, string phoneCode = "7");

        //Предварительная привязка карты
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        string RegisterBinding(string user_key, string phoneCode = "7", int language = 0);

        //Деактивация связки
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        string UnbindCard(string bindingId, string user_key, int language = 0);


        //Активация связки
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        string BindCard(string bindingId, string user_key, int language = 0);

        //Изменение названия связки
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Binding> ChangeCardName(string phoneNumber, string bindingId, string cardName, string user_key, string phoneCode = "7", int language = 0);

        #endregion

        #region Orders

        //Получение информации о заказах стола, привязанных к блюду
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Order> FindOrders(string phoneNumber, string qr, string user_key, string phoneCode = "7", int language = 0);

        //Получение информации о заказе по его номеру
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Order> GetOrder(int restaurantID, string orderNumber, string user_key, string phoneCode = "7", int language = 0);

        //Вызов официанта
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int CallWaiter(int restaurantID, string tableID, string orderNumber, int code, string user_key, string phoneCode = "7", int language = 0);

        //Вызов администратора
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int CallAdmin(int restaurantID, string tableID, string orderNumber, int code, string user_key);

        //Начало оплаты заказа - назначение статуса заказа precheck
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Order> OrderPrecheck(int restaurantID, string orderNumber, decimal paymentSum, Int64? discountCardNumber, string user_key, string phoneCode = "7", int language = 0);

        //Отмена начала оплаты заказа - назначение статуса заказа Открыт
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Order> OrderCancelPrecheck(int restaurantID, string orderNumber, Int64? discountCardNumber, string user_key, string phoneCode = "7", int language = 0);

        //Оплата заказа по связкам
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Order> GetPaymentBinding(int restaurantID, string orderNumber, decimal paymentSum, long paymentBank, string user_key, string bindingId, decimal tippingProcent, string phoneCode = "7", int language = 0);

        //Оплата заказа без связок
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        string GetPayment(int restaurantID, string orderNumber, decimal paymentSum, long paymentBank, string user_key, int language = 0);


        //Проверка статуса заказа
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        string GetOrderStatus(string orderId, string user_key, int language = 0);

        /*
        //Отправка сообщения на email
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int SendMessage(int restaurantID, string orderNumber, string email, string message, string user_key, int language = 0);*/


        //Оценка качества обслуживания в ресторане
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int SaveRating(int restaurantID, string orderNumber, int rating, string user_key);

        #endregion

        #region Restaurant

        //Список ресторанов, подключенных к сети
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<RestNetwork> FindRestaurant(string user_key, int language = 0);

        //Информация о ресторане
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        Restaurant RestaurantInfo(int restaurantID, string user_key, int language = 0);

        #endregion

        #region Personal

        //Авторизация
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        Personal PersonalLogin(string login, string psw);

        //Напоминание пароля 
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int PswRemember(string login);

        //Получение профиля
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        Profile GetProfile(int restaurantID);

        //Смена пароля администратора/официанта
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int ChangePersonalPsw(int personalID, string newPsw);

        //Получение списка сообщений
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Messages> GetMessages(int restaurantID, int mode);

        //Получение списка сообщений
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<Messages> GetMessagesPersonal(int restaurantID, int mode, int personalID);

        //Закрытие сообщения - прооставление статуса прочитано
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        int ReadMessage(int messageID, int personalID);

        //Получение истории платежей за сутки
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<HistoryPayments> GetHistoryPayments(int restaurantID);


        //Получение истории платежей за сутки
        [OperationContract]
        [WebInvoke(Method = "PUT",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        UriTemplate = "get")]
        List<HistoryPayments> GetHistoryPaymentsPersonal(int restaurantID, int personalID);
        #endregion
    }


}
