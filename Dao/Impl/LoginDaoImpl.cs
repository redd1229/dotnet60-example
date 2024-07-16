using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet60_example.Dao.Impl
{
    public class LoginDaoImpl : ILoginDao
    {
        public IQueryable<Account> GetAccountByUserId(ApdbContext context, string userId)
        {
            return context.Account.Where(x => x.UserId!.Equals(userId))
                                  .Where(x => x.IsActive == true)
                                  .Include(x => x.Roles)
                                  .ThenInclude(x => x.Permissions);
        }

        public IQueryable<EmpData> GetEmpDataById(HrdbContext context, string Id)
        {
            return context.EmpData.Where(x => x.OffWork == "N")
                                  .Where(x => x.Id!.Equals(Id));
        }
    }
}
