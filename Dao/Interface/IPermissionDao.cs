using dotnet60_example.Contexts;
using dotnet60_example.Entities;

namespace dotnet60_example.Dao.Interface
{
    public interface IPermissionDao
    {
        IQueryable<Permission> GetAll(ApdbContext context);

        IQueryable<Permission> GetById(ApdbContext context, int permissionId);

        IQueryable<Permission> GetByEntity(ApdbContext context, Permission entity);

        void Insert(ApdbContext context, Permission entity);

        void Update(ApdbContext context, Permission entity);

        void Delete(ApdbContext context, Permission entity);

        void EnabledPermission(ApdbContext context, Permission entity);

        void DisabledPermission(ApdbContext context, Permission entity);
    }
}
