using System;

namespace Ajupov.Infrastructure.All.HotStorage.HotStorage
{
    public interface IHotStorage
    {
        void SetValue<T>(string key, T value, TimeSpan timeSpan);

        bool IsExist(string key);

        T GetValue<T>(string key);
    }
}
