namespace Domain.Common
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public int Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
