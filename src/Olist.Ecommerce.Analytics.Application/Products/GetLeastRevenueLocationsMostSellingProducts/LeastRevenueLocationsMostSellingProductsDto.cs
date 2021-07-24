using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    public class LeastRevenueLocationsMostSellingProductsDto
    {
        public string ProductId { get; set; }
        public string State { get; set; }
        public int RankWithinState { get; set; }
        public float SalesPerProduct { get; set; }
    }
}
