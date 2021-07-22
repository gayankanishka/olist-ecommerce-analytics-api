namespace Olist.Ecommerce.Analytics.Domain.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public int NameLength { get; set; }
        public int DescriptionLength { get; set; }
        public int PhotosQuantity { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
