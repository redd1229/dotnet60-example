using dotnet60_example.Entities;

namespace dotnet60_example.ViewModels
{
    public class AccountCreateVM
    {
        public string? UserId { get; set; }

        public Role Role { get; set; }
    }
}
