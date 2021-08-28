namespace Olist.Ecommerce.Analytics.Domain.Models
{
    /// <summary>
    /// Sales percentages model.
    /// </summary>
    public class SalesPercentage
    {
        /// <summary>
        /// Product ID.
        /// </summary>
        public string ProductId { get; set; }
        
        /// <summary>
        /// Product category name.
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Sales percentage.
        /// </summary>
        public double Percentage { get; set; }
        
        /// <summary>
        /// Sales amount.
        /// </summary>
        public double SalesAmount { get; set; }
        
        /// <summary>
        /// Product rank.
        /// </summary>
        public int Rank { get; set; }
    }
}
