using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;
using dotnet60_example.Helpers;

namespace dotnet60_example.Dao.Impl
{
    public class AccountDaoImpl : IAccountDao
    {
        private readonly SessionHelper _sessionHelper;

        public AccountDaoImpl(SessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
        }

        public IQueryable<Account> GetById(ApdbContext context, string userId)
        {
            return context.Account.Where(x => x.UserId == userId);
        }

        public IQueryable<Account> GetByEntity(ApdbContext context, Account entity)
        {
            IQueryable<Account> query = context.Account;

            if (!string.IsNullOrEmpty(entity.UserId))
            {
                query = query.Where(x => x.UserId!.Contains(entity.UserId));
            }

            if (!string.IsNullOrEmpty(entity.UserName))
            {
                query = query.Where(x => x.UserName!.Contains(entity.UserName));
            }

            if (entity.IsActive is not null)
            {
                query = query.Where(x => x.IsActive == entity.IsActive);
            }

            return query;
        }

        public void Insert(ApdbContext context, Account entity)
        {
            entity.CreateUser = _sessionHelper.GetUserSession().UserId;
            entity.CreateDate = DateTime.Now;
            entity.IsActive = true;
            context.Account.Add(entity);
            context.SaveChanges();
        }

        public void Update(ApdbContext context, Account entity)
        {
            entity.UpdateUser = _sessionHelper.GetUserSession().UserId;
            entity.UpdateDate = DateTime.Now;
            context.SaveChanges();
        }

        public void Delete(ApdbContext context, Account entity)
        {
            context.Account.Remove(entity);
            context.SaveChanges();
        }

        public void EnabledAccount(ApdbContext context, Account entity)
        {
            entity.IsActive = true;
            entity.UpdateUser = _sessionHelper.GetUserSession().UserId;
            entity.UpdateDate = DateTime.Now;
            context.SaveChanges();
        }

        public void DisabledAccount(ApdbContext context, Account entity)
        {
            entity.IsActive = false;
            entity.UpdateUser = _sessionHelper.GetUserSession().UserId;
            entity.UpdateDate = DateTime.Now;
            context.SaveChanges();
        }
    }
}
