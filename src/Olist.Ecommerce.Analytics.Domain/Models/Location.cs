namespace Olist.Ecommerce.Analytics.Domain.Models
{
    /// <summary>
    /// Location model.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// City name.
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Location net revenue.
        /// </summary>
        public double Revenue { get; set; }
        
        /// <summary>
        /// Location rank within the state.
        /// </summary>
        public int Rank { get; set; }
    }
}
