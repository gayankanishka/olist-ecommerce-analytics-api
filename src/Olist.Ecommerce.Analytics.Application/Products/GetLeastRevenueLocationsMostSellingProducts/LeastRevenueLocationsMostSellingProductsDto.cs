using System;

namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    public class LeastRevenueLocationsMostSellingProductsDto
    {
        public double SalesPerProduct { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int RankWithinState { get; set; }
    }
}
