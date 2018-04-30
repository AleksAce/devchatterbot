using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using DevChatter.Bot.Core.Data.Model;

namespace DevChatter.Bot.Core.Data.Specifications
{
    public class DataItemPolicy<T> : ISpecification<T> where T : DataEntity
    {
        protected DataItemPolicy(Expression<Func<T, bool>> expression, string cacheKey = null)
        {
            Criteria = expression;
            _cacheKey = cacheKey;
        }

        public static DataItemPolicy<T> All()
        {
            return new DataItemPolicy<T>(x => true, MakeStaticHelperCacheKey(""));
        }

        public Expression<Func<T, bool>> Criteria { get; }
        private string _cacheKey;
        public string CacheKey => _cacheKey ?? (_cacheKey = $"{typeof(T).Name}-{Criteria}");

        protected static string MakeStaticHelperCacheKey(string criteriaPart, [CallerMemberName] string callerName = "")
        {
            return $"{ typeof(T).Name }-{callerName}-{criteriaPart}";
        }

    }
}
