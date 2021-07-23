namespace Olist.Ecommerce.Analytics.Domain.Models
{
    public class SalesPercentage
    {
        public string ProductId { get; set; }
        public string CategoryName { get; set; }
        public double Value { get; set; }
        public double SalesAmount { get; set; }
    }
}
