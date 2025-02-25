using Domain.Entities;

namespace Application.DTOs
{
    public class CategoryDto
    {
        public required int ParentId { get; set; }
        public required string Name { get; set; }
        public required int Priority { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
