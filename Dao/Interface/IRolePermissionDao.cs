using dotnet60_example.Contexts;
using dotnet60_example.Entities;

namespace dotnet60_example.Dao.Interface
{
    public interface IRolePermissionDao
    {
        void Delete(ApdbContext context, IList<RolePermission> entities);

        void Insert(ApdbContext context, RolePermission entity);
    }
}
