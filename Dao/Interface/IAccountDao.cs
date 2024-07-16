using dotnet60_example.Contexts;
using dotnet60_example.Entities;

namespace dotnet60_example.Dao.Interface
{
    public interface IAccountDao
    {
        IQueryable<Account> GetById(ApdbContext context, string userId);

        IQueryable<Account> GetByEntity(ApdbContext context, Account entity);

        void Insert(ApdbContext context, Account entity);

        void Update(ApdbContext context, Account entity);

        void Delete(ApdbContext context, Account entity);

        void EnabledAccount(ApdbContext context, Account entity);

        void DisabledAccount(ApdbContext context, Account entity);
    }
}
