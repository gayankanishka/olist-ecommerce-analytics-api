namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    /// <summary>
    /// Handles the cache storage operations.
    /// </summary>
    public interface ICacheStore
    {
        /// <summary>
        /// Add a new item to the cache store.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        void AddItem<T>(string cacheKey, T item) where T : class;
        
        /// <summary>
        /// Get an item from the cache store.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Cached item.</returns>
        T? GetItem<T>(string cacheKey) where T : class;
        
        /// <summary>
        /// Remove an item from the cache store.
        /// </summary>
        /// <param name="cacheKey"></param>
        void RemoveItem(string cacheKey);
        
        /// <summary>
        /// Clears the cache store.
        /// </summary>
        void Flush();
    }
}