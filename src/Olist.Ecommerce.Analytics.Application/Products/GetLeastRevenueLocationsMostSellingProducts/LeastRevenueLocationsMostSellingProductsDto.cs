namespace Olist.Ecommerce.Analytics.Application.Products.GetLeastRevenueLocationsMostSellingProducts
{
    public class LeastRevenueLocationsMostSellingProductsDto
    {
        public double SalesPerProduct { get; set; }
        public string ProductId { get; set; }
        public string State { get; set; }
        public int RankWithinState { get; set; }
    }
}
