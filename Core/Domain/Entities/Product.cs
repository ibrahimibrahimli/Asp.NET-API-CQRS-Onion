using Domain.Common;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            
        }
        public Product(string title, string description, decimal price, int brandId, decimal discount)
        {
            Title = title;
            Description = description;
            Price = price;
            BrandId = brandId;
            Discount = discount;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        //public required string ImagePath { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public virtual Brand Brand { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
