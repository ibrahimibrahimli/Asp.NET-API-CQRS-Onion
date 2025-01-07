using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            
        }

        public Category(int parentId, string name, int priority)
        {
            ParentId = parentId;
            Name = name;
            Priority = priority;
        }
        public required int ParentId { get; set; }
        public required string Name { get; set; }
        public required int Priority { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
