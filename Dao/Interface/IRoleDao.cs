using dotnet60_example.Contexts;
using dotnet60_example.Entities;

namespace dotnet60_example.Dao.Interface
{
    public interface IRoleDao
    {
        IQueryable<Role> GetAll(ApdbContext context);

        IQueryable<Role> GetById(ApdbContext context, int roleId);

        IQueryable<Role> GetByEntity(ApdbContext context, Role entity);

        void Insert(ApdbContext context, Role entity);

        void Update(ApdbContext context, Role entity);

        void Delete(ApdbContext context, Role entity);

        void EnabledRole(ApdbContext context, Role entity);

        void DisabledRole(ApdbContext context, Role entity);
    }
}
