using AutoMapper;
using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;
using dotnet60_example.Service.Interface;
using dotnet60_example.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace dotnet60_example.Service.Impl
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountDao _accountDao;
        private readonly ILoginDao _loginDao;
        private readonly IRoleDao _roleDao;
        private readonly IAccountRoleDao _accountRoleDao;
        private readonly ApdbContext _apdbContext;
        private readonly HrdbContext _hrdbContext;

        public AccountServiceImpl(IMapper mapper,
                                  IAccountDao accountDao,
                                  ILoginDao loginDao,
                                  IRoleDao roleDao,
                                  IAccountRoleDao accountRoleDao,
                                  ApdbContext apdbContext,
                                  HrdbContext hrdbContext)
        {
            _mapper = mapper;
            _accountDao = accountDao;
            _loginDao = loginDao;
            _roleDao = roleDao;
            _accountRoleDao = accountRoleDao;
            _hrdbContext = hrdbContext;
            _apdbContext = apdbContext;
        }

        public IList<AccountVM> SearchAccount(AccountSearchVM searchVM)
        {
            var entity = _mapper.Map<Account>(searchVM);
            var list = _accountDao.GetByEntity(_apdbContext, entity);
            return _mapper.Map<IList<AccountVM>>(list);
        }

        public ResultVM SearchHrAccount(string userId)
        {
            var entity = _loginDao.GetEmpDataById(_hrdbContext, userId).FirstOrDefault();
            if (entity is null)
            {
                return new ResultVM
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = Resources.Language.NoData
                };
            }
            return new ResultVM
            {
                StatusCode = HttpStatusCode.OK,
                Message = Resources.Language.Success,
                Body = _mapper.Map<EmpDataVM>(entity)
            };
        }

        public ResultVM GetAccount(string userId)
        {
            var entity = _accountDao.GetById(_apdbContext, userId)
                                    .Include(x => x.Roles)
                                    .FirstOrDefault();
            if (entity is null)
            {
                return new ResultVM
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = Resources.Language.AccountNotFound
                };
            }

            var roles = _roleDao.GetAll(_apdbContext).OrderBy(x => x.Description).ToList();
            var selectRoles = entity.Roles.OrderBy(x => x.Description);
            var unselectRoles = roles.Except(selectRoles).OrderBy(x => x.Description).ToList();

            return new ResultVM
            {
                StatusCode = HttpStatusCode.OK,
                Message = Resources.Language.Success,
                Body = new
                {
                    account = _mapper.Map<AccountVM>(entity),
                    selectRoles = _mapper.Map<IList<RoleVM>>(selectRoles),
                    unselectRoles = _mapper.Map<IList<RoleVM>>(unselectRoles)
                }
            };
        }

        public ResultVM CreateAccount(AccountCreateVM createVM)
        {
            using var tran = _apdbContext.Database.BeginTransaction();
            try
            {
                var entity = _accountDao.GetById(_apdbContext, createVM.UserId)
                                        .FirstOrDefault();

                if (entity is not null)
                {
                    if (entity.IsActive == true)
                    {
                        return new ResultVM
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = Resources.Language.AccountAlreadyExists,
                        };
                    }
                    _accountDao.EnabledAccount(_apdbContext, entity);
                }
                else
                {
                    var hrAccount = _loginDao.GetEmpDataById(_hrdbContext, createVM.UserId).FirstOrDefault();
                    var newAccount = _mapper.Map<Account>(hrAccount);
                    _accountDao.Insert(_apdbContext, newAccount);
                }

                tran.Commit();
                return new ResultVM
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = Resources.Language.SaveSuccess,
                };
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public ResultVM DeleteAccount(string userId)
        {
            using var tran = _apdbContext.Database.BeginTransaction();
            try
            {
                //Include AccountRoles Cascade Delete
                var entity = _accountDao.GetById(_apdbContext, userId)
                                        .Include(x => x.AccountRoles)
                                        .FirstOrDefault();

                if (entity is not null)
                {
                    _accountDao.Delete(_apdbContext, entity);
                }

                tran.Commit();
                return new ResultVM
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = Resources.Language.DeleteSuccess,
                };
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public ResultVM EditAccount(AccountEditVM editVM)
        {
            using var tran = _apdbContext.Database.BeginTransaction();
            try
            {
                var entity = _accountDao.GetById(_apdbContext, editVM.UserId)
                                        .Include(x => x.AccountRoles)
                                        .FirstOrDefault();

                if (entity is not null)
                {
                    _mapper.Map(editVM, entity);
                    _accountDao.Update(_apdbContext, entity);

                    _accountRoleDao.Delete(_apdbContext, entity.AccountRoles);

                    editVM.RoleIds.ToList().ForEach(x =>
                    {
                        _accountRoleDao.Insert(_apdbContext, new AccountRole
                        {
                            RoleId = x,
                            UserId = entity.UserId,
                        });
                    });
                }

                tran.Commit();
                return new ResultVM
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = Resources.Language.EditSuccess,
                };
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}