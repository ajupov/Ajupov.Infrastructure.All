using System;

namespace Infrastructure.All.HotStorage.HotStorage
{
    public interface IHotStorage
    {
        void SetTempString(string value, TimeSpan timeSpan);

        void SetValue<T>(string key, T value, TimeSpan timeSpan);

        bool IsExist(string key);

        T GetValue<T>(string key);
    }
}