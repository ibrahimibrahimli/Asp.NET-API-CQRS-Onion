using Domain.Common;

namespace Domain.Entities
{
    public class Brand : BaseEntity
    {
        public Brand()
        {
            
        }

        public Brand(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
