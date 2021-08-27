using System;
using Microsoft.Extensions.Caching.Memory;
using Olist.Ecommerce.Analytics.Application.Common.Interfaces;

namespace Olist.Ecommerce.Analytics.Infrastructure.Caching
{
     /// <summary>
    /// Handles the cache storage operations.
    /// </summary>
    public class CacheStore : ICacheStore
    {
        // TODO | GK: Add redis support and treat this as a distributed caching wrapper.
        
        private readonly IMemoryCache _memoryCache;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="memoryCache"></param>
        public CacheStore(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        /// <inheritdoc />
        public void AddItem<T>(string cacheKey, T item) where T : class
        {
            MemoryCacheEntryOptions cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.UtcNow.AddMinutes(60),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };
            
            _memoryCache.Set(cacheKey, item, cacheExpiryOptions);
        }
        
        /// <inheritdoc />
        public void AddItem<T>(string cacheKey, T item, MemoryCacheEntryOptions options) where T : class
        {
            _memoryCache.Set(cacheKey, item, options);
        }

        /// <inheritdoc />
        public T? GetItem<T>(string cacheKey) where T : class
        {
            return _memoryCache.TryGetValue(cacheKey, out T? item) ? item : null;
        }

        /// <inheritdoc />
        public void RemoveItem(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

        /// <inheritdoc />
        public void Flush()
        {
            if (_memoryCache is not MemoryCache memoryCache)
            {
                throw new NullReferenceException("Cache Not Found!");
            }

            double percentage = 1.0;
            memoryCache.Compact(percentage);
        }
    }
}