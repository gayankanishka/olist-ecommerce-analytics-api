namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    public class LeastRevenueLocationsMostSellingProductsDto
    {
        public string ProductId { get; set; }
        public string State { get; set; }
        public int RankWithinState { get; set; }
        public double SalesPerProduct { get; set; }
    }
}
