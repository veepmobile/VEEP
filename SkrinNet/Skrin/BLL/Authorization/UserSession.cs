using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace Skrin.BLL.Authorization
{
    public class UserSession
    {
        private string _session_id;
        private SkrinUser _user;
        private HttpContextBase _context;
        private AuthenticationType _auth_result = AuthenticationType.NotFound;
        private const string SESSION_COOKIE = "user_session_id";
        private IUserSessionRepository rep;



        public UserSession(HttpContextBase context)
        {
#if LOCAL_TEST
            rep = new FakeSessionRepository();
#else
            rep = new SqlRedisCachedSessionRepository();
#endif

            _context = context;
            _session_id = CookieHelper.GetCookieVal(context.Request, SESSION_COOKIE);

            //Если IP заблокирован, то больше ничего не проверяем
            if (rep.IsBlackIp(Utilites.GetIP(_context)))
            {
                _auth_result=AuthenticationType.IsBlockedIp;
                return;
            }


            //Если у пользователя нет в куки записи о sessionId то вернем AuthenticationType.NotFound
            if (string.IsNullOrEmpty(_session_id))
                return;



            if (!string.IsNullOrEmpty(_session_id))
            {
                _user = rep.GetUser(_session_id);
                //Если пользователь не определен по данной _sessionId , то удалим запись из куки и вернем AuthenticationType.NotFound
                if (_user == null)
                {
                    Kill();
                    return;
                }
            }

            //Если у данного пользователя есть активная запись с другим sessionId,  то удалим запись из куки и вернем AuthenticationType.NotFound           
            if (rep.UserHaveAnotherActiveSession(_user, _session_id))
            {
                Kill();
                return;
            }

            //Проведем авторизацию пользователя
            _auth_result = Authorize();
            if (_auth_result == AuthenticationType.Ok)
            {
                MakeActive();
            }
        }

        /// <summary>
        /// Пользователь Скрина
        /// </summary>
        public SkrinUser User
        {
            get { return _user; }
        }

        /// <summary>
        /// Id пользователя Скрина (0 - в случае анонима)
        /// </summary>
        public int UserId
        {
            get { return _user == null ? 0 : _user.Id; }
        }

        /// <summary>
        /// Лимит мониторинга выписок ЕГРЮЛ
        /// </summary>
        public int EgrulMonitorLimit
        {
            get { return _user == null ? 0 : _user.EgrulMonitorLimit; }
        }
        /// <summary>
        /// Лимит групп для мониторинга сообщений
        /// </summary>
        public int MessageGroupLimit
        {
            get { return _user == null ? 0 : _user.MessageGroupLimit; }
        }
        /// <summary>
        /// Лимит групп для мониторинга обновлений
        /// </summary>
        public int UpdateGroupLimit
        {
            get { return _user == null ? 0 : _user.UpdateGroupLimit; }
        }
        /// <summary>
        /// Лимит групп для мониторинга ЕГРЮЛ
        /// </summary>
        public int EgrulGroupLimit
        {
            get { return _user == null ? 0 : _user.EgrulGroupLimit; }
        }
        /// <summary>
        /// Лимит добавления в группу
        /// </summary>
        public int GroupLimit
        {
            get { return _user == null ? 0 : _user.GroupLimit; }
        }

        /// <summary>
        /// Идентификатор сессии пользователя
        /// </summary>
        public string SessionId
        {
            get { return _session_id; }
        }

        /// <summary>
        /// Результат Аунтификации и Авторизации пользователя
        /// </summary>
        public AuthenticationType AuthenticationResult
        {
            get { return _auth_result; }
        }

        public AuthenticationType Login(string login, string password)
        {
            AuthLog log = new AuthLog(login, password, Utilites.GetIP(_context), _context.Request.Browser.Browser + " " + _context.Request.Browser.Version);
            try
            {
                // TODO добавить проверку кол-ва неправильных попыток входа.


                log.stages.Add("Проверка логина/пароля на заполнение");

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    _auth_result = AuthenticationType.NotFound;
                    return LogResult(_auth_result,log);
                }

                log.stages.Add("Проверка логина/пароля на правильность");
                _user = rep.GetUser(login, password);
                if (_user == null)
                {

                    log.stages.Add("Проверка превышение кол-ва неправильных попыток ввода");

                    //проверим превышение кол-ва неправильных попыток ввода
                    if (rep.BlockForErrorLogin(login))
                    {
                        _auth_result = AuthenticationType.PasswordAttempCountExcees;
                        return LogResult(_auth_result, log);
                    }
                    _auth_result = AuthenticationType.NotFound;
                    return LogResult(_auth_result, log);
                }

                _session_id = _context.Session.SessionID + "_" + _user.Id + "_0";
                log.session_id = _session_id;


                log.stages.Add("Проверка есть ли работающий пользователь");

                //Если уже есть работающий пользователь то новому не даем залогинится
                if (rep.UserHaveAnotherActiveSession(_user, _session_id))
                {
                    

                    //Проверим есть ли запрос на удаление активной сессии
                    log.stages.Add("Проверка есть ли запрос на удаление активной сессии");
                    if(rep.UserInUnlockQueue(_user.Id))
                    {
                        //Убиваем другю активную сессию
                        log.stages.Add("Убиваем другю активную сессию");
                        rep.KillActiveSession(_user);
                    }
                    else
                    {
                        _auth_result = AuthenticationType.PasswordUsed;
                        return LogResult(_auth_result, log);
                    }
                    
                }

                log.stages.Add("Проверка есть ли привязка к компу");

                //Пользователей привязанных к компу проверим на привязку
                if (_user.UsePCBinding && !IsRightPC())
                {
                    _auth_result = AuthenticationType.BindingError;
                    return LogResult(_auth_result, log);
                }

                //Проведем авторизацию пользователя
                _auth_result = Authorize();

                if (_auth_result == AuthenticationType.Ok)
                {
                    MakeActive();
                }

                return LogResult(_auth_result, log);
            }
            catch(Exception ex)
            {
                log.exception = ex.Message;
                LogResult(_auth_result, log);
                throw;
            }
        }


        private AuthenticationType LogResult(AuthenticationType result, AuthLog log)
        {
            log.auth_result = result;
            rep.LogAuthResult(log);
            return result;
        }


        public AuthenticationType AuthenticateExtenal(SkrinUser user,UserSource source)
        {
            _user = user;

            if (_user == null)
            {
                return _auth_result = AuthenticationType.NotFound;
            }

            _session_id = _context.Session.SessionID + "_" + _user.Id + "_"+(int)source;
            
            //Если уже есть работающий пользователь то новому не даем залогинится
            if (rep.UserHaveAnotherActiveSession(_user, _session_id))
            {
                _auth_result = AuthenticationType.PasswordUsed;
                return _auth_result;
            }

            MakeActive();

            return _auth_result = AuthenticationType.Ok;
        }

        public void Logout()
        {
            rep.KillUserSession(_session_id);
            Kill();
            _user = null;
            _auth_result = AuthenticationType.NotFound;
        }

        /// <summary>
        /// Проверяет интенсивность запросов и блокирует слишком интенсивные
        /// </summary>
        public void CheckRequestCount()
        {
            string ip=Utilites.GetIP(_context);
            string user = UserId.ToString();
            int request_count = rep.AddRequest(ip,user);
            if(request_count>60)
            {
                
                rep.BlockIp(ip,user);
                _auth_result = AuthenticationType.IsBlockedIp;
            }
        }

        /// <summary>
        /// Имеет ли пользователь права для данной роли
        /// </summary>
        /// <param name="collection">Список ролей с разрешенными типами доступа</param>
        /// <param name="role">роль, необходимая для проверки</param>
        /// <returns></returns>
        public bool HasRole(Dictionary<string, AccessType> collection, string role,TestConsideration allow_test=TestConsideration.All)
        {
            if (_user == null)
                return false;
            foreach(var access in _user.SitesAccess.Where(p=>!p.IsOutOfDate).Where(p=>allow_test==TestConsideration.All||p.IsTest==false).Select(p=>p.AccessType))
            {
                if ((collection[role] & access) == access)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Выдает список прав для данного пользователя
        /// </summary>
        /// <param name="collection">Список ролей с разрешенными типами доступа</param>
        /// <returns></returns>
        public string GetRigthList(Dictionary<string, AccessType> collection)
        {

            List<string> rows = new List<string>();
            foreach (var item in collection)
            {
                rows.Add(string.Format("\"{0}\":{1}", item.Key, HasRole(collection, item.Key) ? 1 : 0));
            }

            return string.Format("{{{0}}}", string.Join(",", rows));

        }

        /// <summary>
        /// Подтверждение пользованием облаком
        /// </summary>
        public void ConfirmCloudUsing()
        {
            rep.ConfirmCloudUsing(_user.Id);
        }


        #region private

        /// <summary>
        /// Удаление куки пользователя
        /// </summary>
        private void Kill()
        {
            CookieHelper.RemoveCookie(_context.Response, SESSION_COOKIE);
            _session_id = null;
        }

        /// <summary>
        /// Обновление текущей сессии
        /// </summary>
        private void MakeActive()
        {
            //Обновление сессии в базе
            rep.UpdateUserSession(_user, _session_id);
            //Обновление cookie 
            CookieHelper.AddCookie(_context.Response, SESSION_COOKIE, _session_id, DateTime.Now.AddYears(1));
            //Удаление других записей с данным пользователем
            rep.DeleteOldSessions(_user, _session_id);
        }

        private AuthenticationType Authorize(List<string> stages=null)
        {
            stages = stages ?? new List<string>();
            stages.Add("Проверка пользователя на наличие подписок");
            //проверим пользователя на наличие подписок
            if (_user.SitesAccess.Count == 0)
                return AuthenticationType.NoAccessType;
            stages.Add("Проверка пользователя на черный список");
            //проверим пользователя на черный список            
            if (_user.IsBlocked)
                return AuthenticationType.IsBlockedUser;
            stages.Add("Проверка пользователя на наличие активных подписок");
            //проверим пользователя на наличие активных подписок
            if (!UserHaveActualSubscription())
                return AuthenticationType.IsOutOfDate;
            stages.Add("Проверка пользователя по ограничению по IP");
            //проверка по ограничению по IP
            if (_user.HasIpRestriction && !_user.AllowedIps.Contains(Utilites.GetIP(_context)))
                return AuthenticationType.DeniedIP;
            return AuthenticationType.Ok;
        }


        /// <summary>
        /// Пользователь имеет хотя бы одну актуальную подписку
        /// </summary>
        /// <returns></returns>
        private bool UserHaveActualSubscription()
        {
            foreach (Access a in _user.SitesAccess)
            {
                if (!a.IsOutOfDate)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Пользователь заходит с правильного компа
        /// </summary>
        /// <returns></returns>
        private bool IsRightPC()
        {
            string user_ip = Utilites.GetIP(_context);
            string user_browser = _context.Request.Browser.Browser;
            if (string.IsNullOrEmpty(_user.BoundIp))
            {
                _user.BoundIp = user_ip;
                _user.BoundBrowser = user_browser;
                rep.UpdateUser(_user);
                return true;
            }
            return _user.BoundIp == user_ip && _user.BoundBrowser == user_browser;
        }

        #endregion

    }

    public enum AuthenticationType
    {
        Ok = 0,
        [Description("Неверный логин или пароль")]
        NotFound = 1,
        [Description("Пользователь с данным логином уже работает")]
        PasswordUsed = 2,
        [Description("Данный логин привязан к другому компьютеру")]
        BindingError = 3,
        [Description("Данный пользователь заблокирован")]
        IsBlockedUser = 4,
        [Description("У пользователя нет ни одной действующей подписки")]
        IsOutOfDate = 5,
        [Description("Отсутствует разрешение для входа с данного IP")]
        DeniedIP = 6,
        [Description("У пользователя отсутствуют подписки")]
        NoAccessType=7,
        [Description("Данный Ip заблокирован")]
        IsBlockedIp = 8,
        [Description("Превышено кол-во попыток неправильного ввода пароля")]
        PasswordAttempCountExcees
    }

    /// <summary>
    /// Типы доступа, доступные на сайте СКРИН
    /// </summary>
    public enum AccessType
    {
        /// <summary>
        /// Скрин предприятие
        /// </summary>
        [Description("Скрин предприятие")]
        Pred = 1 << 0,
        /// <summary>
        /// Скрин эмитент
        /// </summary>
        [Description("Скрин эмитент")]
        Emitent = 1 << 1,
        /// <summary>
        /// Блумберг
        /// </summary>
        [Description("Блумберг")]
        Bloom = 1 << 2,
        /// <summary>
        /// Сообщения
        /// </summary>
        [Description("Сообщения")]
        Mess = 1 << 3,
        /// <summary>
        /// Дил+У
        /// </summary>
        [Description("Дил+У")]
        Deal = 1 << 4,
        /// <summary>
        /// Контрагент базовый
        /// </summary>
        [Description("Контрагент базовый")]
        Ka = 1 << 5,
        /// <summary>
        /// Контрагент расширенный
        /// </summary>
        [Description("Контрагент расширенный")]
        KaPlus = 1 << 6,
        /// <summary>
        /// Контрагент полный
        /// </summary>
        [Description("Контрагент полный")]
        KaPoln = 1 << 7,
        /// <summary>
        /// Мониторинг ЕГРЮЛ
        /// </summary>
        [Description("Мониторинг ЕГРЮЛ")]
        MonEgrul = 1 << 8,
        /// <summary>
        /// Трансфертное ценообразование
        /// </summary>
        [Description("Трансфертное ценообразование")]
        TPrice = 1 << 9
    };

    /// <summary>
    /// Откуда пользователь пришел
    /// </summary>
    public enum UserSource
    {
        Skrin=0,
        Rts44=1,
        Rts223=2,
        B2B=3
    }

    /// <summary>
    /// Учитывать ли тестовые учетные записи
    /// </summary>
    public enum TestConsideration
    {
        /// <summary>
        /// Учитывать все
        /// </summary>
        All=0,
        /// <summary>
        /// Не учитывать тестовые
        /// </summary>
        NotTest=1
    }

}