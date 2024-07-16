using dotnet60_example.Contexts;
using dotnet60_example.Entities;

namespace dotnet60_example.Dao.Interface
{
    public interface ILoginDao
    {
        IQueryable<Account> GetAccountByUserId(ApdbContext context, string userId);

        IQueryable<EmpData> GetEmpDataById(HrdbContext context, string Id);
    }
}
