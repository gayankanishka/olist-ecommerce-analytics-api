namespace Olist.Ecommerce.Analytics.Domain.Models
{
    public class SalesPercentage
    {
        public string ProductId { get; set; }
        public string CategoryName { get; set; }
        public double Percentage { get; set; }
        public double SalesAmount { get; set; }
        public int Rank { get; set; }
    }
}
