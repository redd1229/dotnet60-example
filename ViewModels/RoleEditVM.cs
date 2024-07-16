namespace dotnet60_example.ViewModels
{
    public class RoleEditVM
    {
        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public IList<int> PermissionIds { get; set; }
    }
}
