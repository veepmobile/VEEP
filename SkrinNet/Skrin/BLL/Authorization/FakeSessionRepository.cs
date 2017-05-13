using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Authorization
{
    public class FakeSessionRepository : IUserSessionRepository
    {
        private static List<SkrinUser> user_list = new List<SkrinUser>{
            new SkrinUser
            {
                Login = "kitaev",
                Password = "test",
                Id = 889,
                UsePCBinding = false,
                HasIpRestriction=false,
                SitesAccess=new List<Access>{
                    {new Access{AccessType=AccessType.Mess, IsOutOfDate=false}},
                    {new Access{AccessType=AccessType.Pred, IsOutOfDate=false}},
                    {new Access{AccessType=AccessType.KaPlus, IsOutOfDate=false}},
                    {new Access{AccessType=AccessType.Emitent, IsOutOfDate=false}},
                    {new Access{AccessType=AccessType.Deal, IsOutOfDate=false}},
                    {new Access{AccessType=AccessType.Bloom, IsOutOfDate=false}},
                    {new Access{AccessType=AccessType.Ka, IsOutOfDate=false}},
                }
            }
        };

        private static Dictionary<string, int> user_sessions = new Dictionary<string, int>();

        public SkrinUser GetUser(string sessionId)
        {
            SkrinUser user = null;
            int user_id = 0;
            user_sessions.TryGetValue(sessionId.GetCookieSessionId(), out user_id);
            return user_list.Where(p => p.Id == user_id).FirstOrDefault();
        }

        public SkrinUser GetUser(string login, string password)
        {
            return user_list.Where(p => p.Login == login && p.Password == password).FirstOrDefault();
        }


        public bool IsBlackIp(string ip)
        {
            return false;
        }

        public bool UserHaveAnotherActiveSession(SkrinUser user, string sessionId)
        {
            foreach (var kv in user_sessions)
            {
                if (kv.Value == user.Id)
                {
                    return sessionId.GetCookieSessionId() != kv.Key;
                }
            }
            return false;
        }

        public void UpdateUserSession(SkrinUser user, string sessionId)
        {
            if (!user_sessions.Keys.Contains(sessionId.GetCookieSessionId()))
            {
                user_sessions.Add(sessionId.GetCookieSessionId(), user.Id);
            }
        }

        public void DeleteOldSessions(SkrinUser user, string sessionId)
        {
            List<string> keys_for_remove = new List<string>();
            foreach (var kv in user_sessions)
            {
                if (kv.Value == user.Id)
                {
                    if (kv.Key != sessionId.GetCookieSessionId())
                        keys_for_remove.Add(kv.Key);
                }
            }
            foreach (var key in keys_for_remove)
            {
                user_sessions.Remove(key);
            }
        }


        public void KillUserSession(string sessionId)
        {
            if (user_sessions.Keys.Contains(sessionId.GetCookieSessionId()))
            {
                user_sessions.Remove(sessionId.GetCookieSessionId());
            }
        }


        public bool IsBlackUser(SkrinUser user)
        {
            return false;
        }


        public void UpdateUser(SkrinUser user)
        {
            return;
        }


        public bool BlockForErrorLogin(string login)
        {
            return false;
        }


        public int AddRequest(string ip, string user)
        {
            return 1;
            //throw new NotImplementedException();
        }

        public void BlockIp(string ip,string user)
        {
            throw new NotImplementedException();
        }


        public void LogAuthResult(AuthLog log)
        {
            //throw new NotImplementedException();
        }


        public bool UserInUnlockQueue(int user_id)
        {
            throw new NotImplementedException();
        }


        public void KillActiveSession(SkrinUser user)
        {
            throw new NotImplementedException();
        }


        public void ConfirmCloudUsing(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}