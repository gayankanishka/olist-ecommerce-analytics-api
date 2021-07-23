using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    public class LeastRevenueLocationsMostSellingProductsDto
    {
        public Product Product { get; set; }
        public string State { get; set; }
        public int RankWithinState { get; set; }
    }
}
