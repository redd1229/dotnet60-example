using dotnet60_example.Contexts;
using dotnet60_example.Entities;

namespace dotnet60_example.Dao.Interface
{
    public interface IAccountRoleDao
    {
        void Delete(ApdbContext context, IList<AccountRole> entities);

        void Insert(ApdbContext context, AccountRole entity);
    }
}
