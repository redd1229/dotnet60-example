using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;
using dotnet60_example.Helpers;

namespace dotnet60_example.Dao.Impl
{
    public class RolePermissionDaoImpl : IRolePermissionDao
    {
        private readonly SessionHelper _sessionHelper;

        public RolePermissionDaoImpl(SessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
        }

        public void Delete(ApdbContext context, IList<RolePermission> entities)
        {
            context.RemoveRange(entities);
            context.SaveChanges();
        }

        public void Insert(ApdbContext context, RolePermission entity)
        {
            entity.CreateUser = _sessionHelper.GetUserSession().UserId;
            entity.CreateDate = DateTime.Now;
            context.Add(entity);
            context.SaveChanges();
        }
    }
}
