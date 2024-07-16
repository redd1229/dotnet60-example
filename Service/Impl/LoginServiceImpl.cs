using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;
using dotnet60_example.Helpers;
using dotnet60_example.Models;
using dotnet60_example.Service.Interface;

namespace dotnet60_example.Service.Impl
{
    public class LoginServiceImpl : ILoginService
    {
        private readonly ILoginDao _loginDao;
        private readonly SessionHelper _sessionHelper;
        private readonly ApdbContext _apdbContext;
        private readonly HrdbContext _hrdbContext;

        public LoginServiceImpl(ILoginDao loginDao,
                                SessionHelper sessionHelper,
                                ApdbContext apdbContext,
                                HrdbContext hrdbContext)
        {
            _loginDao = loginDao;
            _sessionHelper = sessionHelper;
            _apdbContext = apdbContext;
            _hrdbContext = hrdbContext;
        }

        public bool CheckAccount(string userId, string password)
        {
            var account = _loginDao.GetAccountByUserId(_apdbContext, userId).FirstOrDefault();
            var emp = _loginDao.GetEmpDataById(_hrdbContext, userId).FirstOrDefault();

            if (account is not null && emp is not null &&
                emp.Password.ToLower().Equals(password.ToLower()))
            {
                SetUserSession(account);
                return true;
            }

            return false;
        }

        private void SetUserSession(Account account)
        {
            _sessionHelper.SetUserSession(new UserSession
            {
                UserId = account.UserId,
                UserName = account.UserName,
                UserRoles = account.Roles,
                UserPermissions = account.Roles.SelectMany(x => x.Permissions)
                                               .DistinctBy(x => x.PermissionId)
                                               .ToList(),
            });
        }
    }
}
