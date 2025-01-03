using Domain.Common;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int BrandId { get; set; }
        //public required string ImagePath { get; set; }
        public required decimal Price { get; set; }
        public required decimal Discount { get; set; }

        public virtual Brand Brand { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
