using System;
using System.Collections.Concurrent;

namespace Ajupov.Infrastructure.All.HotStorage.HotStorage
{
    public class InMemoryHotStorage : IHotStorage
    {
        private readonly ConcurrentDictionary<string, object> _dictionary = new ();

        public void SetValue<T>(string key, T value, TimeSpan timeSpan)
        {
            _dictionary.AddOrUpdate(key, value, (_, _) => value);
        }

        public bool IsExist(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public T GetValue<T>(string key)
        {
            return _dictionary.TryGetValue(key, out var value) ? value is T valueAsT ? valueAsT : default : default;
        }
    }
}
