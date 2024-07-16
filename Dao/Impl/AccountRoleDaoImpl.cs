using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;
using dotnet60_example.Helpers;

namespace dotnet60_example.Dao.Impl
{
    public class AccountRoleDaoImpl : IAccountRoleDao
    {
        private readonly SessionHelper _sessionHelper;

        public AccountRoleDaoImpl(SessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
        }

        public void Delete(ApdbContext context, IList<AccountRole> entities)
        {
            context.RemoveRange(entities);
            context.SaveChanges();
        }

        public void Insert(ApdbContext context, AccountRole entity)
        {
            entity.CreateUser = _sessionHelper.GetUserSession().UserId;
            entity.CreateDate = DateTime.Now;
            context.Add(entity);
            context.SaveChanges();
        }
    }
}
