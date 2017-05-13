using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Infrastructure
{
    public static class RedisStore
    {
        private static Lazy<ConfigurationOptions> configOptions
            = new Lazy<ConfigurationOptions>(() =>
            {
                var configOptions = new ConfigurationOptions();
                configOptions.EndPoints.Add(Configs.RedisServer);
                configOptions.ConnectTimeout = 100000;
                configOptions.SyncTimeout = 100000;
                configOptions.AbortOnConnectFail = false;
                return configOptions;
            });

        private static Lazy<ConnectionMultiplexer> conn
        = new Lazy<ConnectionMultiplexer>(
            () => ConnectionMultiplexer.Connect(configOptions.Value));


        public static IDatabase Db
        {
            get
            {
                return conn.Value.GetDatabase();
            }
        }

        public static bool Set(string key, object value, TimeSpan? expiresIn = null)
        {
            var saved_val = JsonConvert.SerializeObject(value);
            return Db.StringSet(key, saved_val, expiresIn);
        }

        /// <summary>
        /// Обновляет значение в кеше, не изменяя его периода действия, если ключа нет,то добавляет данное значение с указанным периодом действия
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIfNotExists">Период действия для несуществующего ключа</param>
        /// <returns></returns>
        public static bool Update(string key, object value, TimeSpan? expiresIfNotExists = null)
        {
            TimeSpan? expire_Time = Db.KeyTimeToLive(key) ?? expiresIfNotExists;
            var saved_val = JsonConvert.SerializeObject(value);
            return Db.StringSet(key, saved_val, expire_Time);
        }

        public static void Del(string key)
        {
            Db.KeyDelete(key);
        }

        public static void Push(string key, object value)
        {
            var saved_val = JsonConvert.SerializeObject(value);
            Db.ListRightPush(key, saved_val);
        }


        public static T PopQueue<T>(string key)
        {
            string saved_val = Db.ListLeftPop(key);
            return saved_val == null ? default(T) : JsonConvert.DeserializeObject<T>(saved_val);
        }

        public static T PopStack<T>(string key)
        {
            string saved_val = Db.ListRightPop(key);
            return saved_val == null ? default(T) : JsonConvert.DeserializeObject<T>(saved_val);
        }

    }

}