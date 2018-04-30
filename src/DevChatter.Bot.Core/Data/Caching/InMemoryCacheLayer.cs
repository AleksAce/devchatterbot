using System.Collections.Generic;

namespace DevChatter.Bot.Core.Data.Caching
{
    // TODO: Create a better ICacheLayer implementation (have this wrap a real one, move to infra)
    public class InMemoryCacheLayer : ICacheLayer
    {
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        public T TryGet<T>(string cacheKey) where T : class
        {
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache[cacheKey] as T;
            }
            return null;
        }

        public void Insert<T>(T item, string cacheKey) where T : class
        {
            _cache[cacheKey] = item;
        }
    }
}
