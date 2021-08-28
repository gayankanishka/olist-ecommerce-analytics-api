using System;

namespace Olist.Ecommerce.Analytics.Application.Sellers.GetMostPopularSellers
{
    /// <summary>
    /// Most popular sellers query.
    /// </summary>
    public class MostPopularSellerDto
    {
        /// <summary>
        /// Sales amount of the seller.
        /// </summary>
        public double SalesPerSeller { get; set; }
        
        /// <summary>
        /// Seller id.
        /// </summary>
        public string SellerId { get; set; } = string.Empty;
        
        /// <summary>
        /// Product id.
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
    }
}
