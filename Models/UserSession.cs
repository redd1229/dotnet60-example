using dotnet60_example.Entities;

namespace dotnet60_example.Models
{
    public class UserSession
    {
        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public IList<Role> UserRoles { get; set; }

        public IList<Permission> UserPermissions { get; set; }

        public bool HasPermission(string controller, string action)
        {
            return UserPermissions.Any(x => x.ControllerName == controller &&
                                            x.ActionName == action);
        }
    }
}
