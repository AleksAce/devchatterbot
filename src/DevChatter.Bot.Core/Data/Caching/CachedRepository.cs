using System.Collections.Generic;
using DevChatter.Bot.Core.Data.Model;
using DevChatter.Bot.Core.Data.Specifications;

namespace DevChatter.Bot.Core.Data.Caching
{
    public class CachedRepository : IRepository
    {
        private IRepository _internalRepo;
        private ICacheLayer _cacheLayer;
        public CachedRepository(IRepository repository, ICacheLayer cacheLayer)
        {
            _internalRepo = repository;
            _cacheLayer = cacheLayer;
        }

        public T Create<T>(T dataItem) where T : DataEntity
        {
            return _internalRepo.Create(dataItem);
        }

        public void Create<T>(List<T> dataItemList) where T : DataEntity
        {
            _internalRepo.Create(dataItemList);
        }

        public List<T> List<T>(ISpecification<T> spec = null) where T : DataEntity
        {
            List<T> item = _cacheLayer.TryGet<List<T>>(spec.CacheKey);
            if (item == null)
            {
                item = _internalRepo.List(spec);
                _cacheLayer.Insert(item, spec.CacheKey);
            }
            return item;
        }

        public void Remove<T>(T dataItem) where T : DataEntity
        {
            _internalRepo.Remove(dataItem);
        }

        public T Single<T>(ISpecification<T> spec) where T : DataEntity
        {
            T item = _cacheLayer.TryGet<T>(spec.CacheKey);
            if (item == null)
            {
                item = _internalRepo.Single(spec);
                _cacheLayer.Insert(item, spec.CacheKey);
            }
            return item;
        }

        public T Update<T>(T dataItem) where T : DataEntity
        {
            return _internalRepo.Update(dataItem);
        }

        public void Update<T>(List<T> dataItemList) where T : DataEntity
        {
            _internalRepo.Update(dataItemList);
        }
    }
}
