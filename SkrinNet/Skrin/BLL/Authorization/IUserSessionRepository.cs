using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skrin.BLL.Authorization
{
    interface IUserSessionRepository
    {
        /// <summary>
        /// Вытаскивает пользователя с данным sessionId
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        SkrinUser GetUser(string sessionId);

        /// <summary>
        /// Вытаскивает пользователя с данным логин/пароль
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SkrinUser GetUser(string login, string password);

        /// <summary>
        /// Ip находится в черном списке
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        bool IsBlackIp(string ip);

        /// <summary>
        ///  Пользователь имеет активную сессию, которая не совпадает с  данным sessionId
        /// </summary>
        /// <param name="user"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        bool UserHaveAnotherActiveSession(SkrinUser user, string sessionId);

        /// <summary>
        /// Обновление/добавление активной сессии 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="sessionId"></param>
        void UpdateUserSession(SkrinUser user, string sessionId);

        /// <summary>
        /// Удаление других записей о сессии данного пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="sessionId"></param>
        void DeleteOldSessions(SkrinUser user, string sessionId);

        /// <summary>
        ///  Обновляем информацию о пользователе
        /// </summary>
        /// <param name="user"></param>
        void UpdateUser(SkrinUser user);

        /// <summary>
        /// Удаляет привязку пользователя к сессии
        /// </summary>
        /// <param name="sessionId"></param>
        void KillUserSession(string sessionId);

        /// <summary>
        /// Добавляет +1 к счетчику неправильных попыток и возращает нужно ли блокировать пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        bool BlockForErrorLogin(string login);

        /// <summary>
        /// Добавляет +1 к счетчику запросов за период времени и возвращает общее кол-во запросов
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        int AddRequest(string ip,string user);

        /// <summary>
        /// Блокирует данный ip и юзера, если он есть
        /// </summary>
        /// <param name="ip"></param>
        void BlockIp(string ip,string user);

        /// <summary>
        /// Записи в логи результата аунтификации
        /// </summary>
        /// <param name="log"></param>
        void LogAuthResult(AuthLog log);
        /// <summary>
        /// Пользователю нужно выбить авторизацию в другом браузере
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        bool UserInUnlockQueue(int user_id);
        /// <summary>
        /// Убить активную сессию пользователя
        /// </summary>
        /// <param name="user"></param>
        void KillActiveSession(SkrinUser user);
        /// <summary>
        /// Подтверждение пользованием облаком
        /// </summary>
        /// <param name="user_id"></param>
        void ConfirmCloudUsing(int user_id);
    }
}
