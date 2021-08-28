namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    /// <summary>
    /// Least revenue location products model.
    /// </summary>
    public class LeastRevenueLocationsMostSellingProductsDto
    {
        /// <summary>
        /// Sales amount per a product.
        /// </summary>
        public double SalesPerProduct { get; set; }
        
        /// <summary>
        /// Product id.
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        
        /// <summary>
        /// Sate name.
        /// </summary>
        public string State { get; set; } = string.Empty;
        
        /// <summary>
        /// Product rank within the sate.
        /// </summary>
        public int RankWithinState { get; set; }
    }
}
