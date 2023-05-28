using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace WebApplicationRedisChache.Util;

public class RedisUtil
{
    private IDistributedCache _cache;

    public RedisUtil(IDistributedCache cache)
    {
        _cache = cache;
    }

    public bool ExistData(string key)
    {
        return _cache.Get(key) != null;
    }

    public T Get<T>(string key)
    {
        byte[] byteData = _cache.Get(key);
        string stringData = Encoding.UTF8.GetString(byteData);

        return JsonSerializer.Deserialize<T>(stringData);
    }

    public void Save<T>(string key, T value)
    {
        string stringData = JsonSerializer.Serialize(value);
        byte[] byteData = Encoding.UTF8.GetBytes(stringData);

        _cache.Set(key, byteData);
    }

    public void Save<T>(string key, T value, int lifeTimeInSeconds)
    {
        string stringData = JsonSerializer.Serialize(value);
        byte[] byteData = Encoding.UTF8.GetBytes(stringData);

        _cache.Set(key, byteData, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(lifeTimeInSeconds)
        });
    }

    public void Delete(string key)
    {
        _cache.Remove(key);
    }
}