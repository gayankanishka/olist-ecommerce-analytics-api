namespace Olist.Ecommerce.Analytics.Domain.Constants
{
    /// <summary>
    /// Holds global cache keys.
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// Most revenue localtions key.
        /// </summary>
        public static string MostRevenueLocations => "_MostRevenueLocations";
        
        /// <summary>
        /// Most popular sellers key.
        /// </summary>
        public static string MostPopularSellers => "_MostPopularSellers";
        
        /// <summary>
        /// Least revenue location's most selling products key.
        /// </summary>
        public static string LeastRevenueLocationsMostSellingProducts => "_LeastRevenueLocationsMostSellingProducts";
        
        /// <summary>
        /// Most sold products using credit cards key.
        /// </summary>
        public static string MostSoldProductsUsingCreditCards => "_MostSoldProductsUsingCreditCards";
        
        /// <summary>
        /// Sales percentages key.
        /// </summary>
        public static string SalesPercentages => "_SalesPercentages";
    }
}
