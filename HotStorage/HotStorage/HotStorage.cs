using System;
using ServiceStack.Redis;

namespace Ajupov.Infrastructure.All.HotStorage.HotStorage
{
    public class HotStorage : IHotStorage
    {
        private readonly IRedisClientsManager _redisClientsManager;

        public HotStorage(IRedisClientsManager redisClientsManager)
        {
            _redisClientsManager = redisClientsManager;
        }

        public void SetTempString(string value, TimeSpan timeSpan)
        {
            using var client = _redisClientsManager.GetClient();
            client.SetValue(value, value, timeSpan);
        }

        public void SetValue<T>(string key, T value, TimeSpan timeSpan)
        {
            using var client = _redisClientsManager.GetClient();
            client.Set(key, value, timeSpan);
        }

        public bool IsExist(string key)
        {
            using var client = _redisClientsManager.GetClient();
            return client.ContainsKey(key);
        }

        public T GetValue<T>(string key)
        {
            using var client = _redisClientsManager.GetClient();
            return client.Get<T>(key);
        }
    }
}