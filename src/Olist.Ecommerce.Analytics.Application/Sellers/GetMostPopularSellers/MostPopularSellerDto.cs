using System;

namespace Olist.Ecommerce.Analytics.Application.Sellers.GetMostPopularSellers
{
    public class MostPopularSellerDto
    {
        public double SalesPerSeller { get; set; }
        public string SellerId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
    }
}
