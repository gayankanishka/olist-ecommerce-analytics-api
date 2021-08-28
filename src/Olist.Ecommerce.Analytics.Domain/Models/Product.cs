namespace Olist.Ecommerce.Analytics.Domain.Models
{
    /// <summary>
    /// Product model.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product id.
        /// </summary>
        public string ProductId { get; set; }
        
        /// <summary>
        /// Product category name.
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Product count.
        /// </summary>
        public int Count { get; set; }
    }
}
