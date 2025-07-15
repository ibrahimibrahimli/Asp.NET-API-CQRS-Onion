namespace Domain.Constants
{
    public class RoleDefinition
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Priority { get; set; }
        public bool IsSystemRole { get; set; }
    }
}
