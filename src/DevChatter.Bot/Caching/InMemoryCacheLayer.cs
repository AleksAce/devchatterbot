using System;
using DevChatter.Bot.Core.Data.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace DevChatter.Bot.Caching
{
    // TODO: Create a better ICacheLayer implementation (have this wrap a real one, move to infra)
    public class InMemoryCacheLayer : ICacheLayer
    {
        private readonly IMemoryCache _cache;

        public InMemoryCacheLayer(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T TryGet<T>(string cacheKey) where T : class
        {
            if (_cache.TryGetValue(cacheKey, out T item))
            {
                return item;
            }

            return null;
        }

        public void Insert<T>(T item, string cacheKey) where T : class
        {
            using (var entry = _cache.CreateEntry(cacheKey))
            {
                entry.Value = 2;
                entry.AbsoluteExpiration = DateTime.UtcNow.AddDays(1);
            }
        }
    }
}
