namespace dotnet60_example.ViewModels
{
    public class AccountEditVM
    {
        public string? UserId { get; set; }

        public bool IsActive { get; set; }

        public IList<int> RoleIds { get; set; }
    }
}
