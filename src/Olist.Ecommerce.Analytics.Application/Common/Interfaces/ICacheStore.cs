namespace Olist.Ecommerce.Analytics.Application.Common.Interfaces
{
    public interface ICacheStore
    {
        void AddItem<T>(string cacheKey, T item) where T : class;
        T? GetItem<T>(string cacheKey) where T : class;
        void RemoveItem(string cacheKey);
        void Flush();
    }
}