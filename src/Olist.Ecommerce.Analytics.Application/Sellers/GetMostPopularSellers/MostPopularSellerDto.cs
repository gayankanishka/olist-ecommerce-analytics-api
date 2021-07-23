using Olist.Ecommerce.Analytics.Domain.Models;

namespace Olist.Ecommerce.Analytics.Application.Sellers.GetMostPopularSellers
{
    public class MostPopularSellerDto
    {
        public Product Product { get; set; }
        public Seller Seller { get; set; }
    }
}
