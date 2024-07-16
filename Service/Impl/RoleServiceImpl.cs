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
    public class RoleServiceImpl : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleDao _roleDao;
        private readonly IRolePermissionDao _rolePermissionDao;
        private readonly IPermissionDao _permissionDao;
        private readonly ApdbContext _apdbContext;

        public RoleServiceImpl(IMapper mapper,
                               IRoleDao roleDao,
                               IRolePermissionDao rolePermissionDao,
                               IPermissionDao permissionDao,
                               ApdbContext apdbContext)
        {
            _mapper = mapper;
            _roleDao = roleDao;
            _rolePermissionDao = rolePermissionDao;
            _permissionDao = permissionDao;
            _apdbContext = apdbContext;
        }

        public IList<RoleVM> SearchRole(RoleSearchVM searchVM)
        {
            var entity = _mapper.Map<Role>(searchVM);
            var list = _roleDao.GetByEntity(_apdbContext, entity)
                               .OrderBy(x => x.RoleName);
            return _mapper.Map<IList<RoleVM>>(list);
        }

        public ResultVM GetRole(int roleId)
        {
            var entity = _roleDao.GetById(_apdbContext, roleId)
                                 .Include(x => x.Permissions)
                                 .FirstOrDefault();
            if (entity is null)
            {
                return new ResultVM
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = Resources.Language.RoleNotFound,
                };
            }

            var permissions = _permissionDao.GetAll(_apdbContext).OrderBy(x => x.Description).ToList();
            var selectPermissions = entity.Permissions.OrderBy(x => x.Description);
            var unselectPermissions = permissions.Except(selectPermissions).OrderBy(x => x.Description).ToList();

            return new ResultVM
            {
                StatusCode = HttpStatusCode.OK,
                Message = Resources.Language.Success,
                Body = new
                {
                    role = _mapper.Map<RoleVM>(entity),
                    selectPermissions = _mapper.Map<IList<PermissionVM>>(selectPermissions),
                    unselectPermissions = _mapper.Map<IList<PermissionVM>>(unselectPermissions)
                }
            };
        }

        public ResultVM CreateRole(RoleCreateVM createVM)
        {
            using var tran = _apdbContext.Database.BeginTransaction();
            try
            {
                var entity = _mapper.Map<Role>(createVM);
                _roleDao.Insert(_apdbContext, entity);

                tran.Commit();
                return new ResultVM
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = Resources.Language.CreateSuccess,
                };
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public ResultVM DeleteRole(int roleId)
        {
            using var tran = _apdbContext.Database.BeginTransaction();
            try
            {
                //Include RolePermissions、AccountRoles Cascade Delete
                var entity = _roleDao.GetById(_apdbContext, roleId)
                                     .Include(x => x.RolePermissions)
                                     .Include(x => x.AccountRoles)
                                     .FirstOrDefault();
                if (entity is not null)
                {
                    _roleDao.Delete(_apdbContext, entity);
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

        public ResultVM EditRole(RoleEditVM editVM)
        {
            using var tran = _apdbContext.Database.BeginTransaction();
            try
            {
                var entity = _roleDao.GetById(_apdbContext, editVM.RoleId)
                                     .Include(x => x.RolePermissions)
                                     .FirstOrDefault();

                if (entity is not null)
                {
                    _mapper.Map(editVM, entity);
                    _roleDao.Update(_apdbContext, entity);

                    _rolePermissionDao.Delete(_apdbContext, entity.RolePermissions);

                    editVM.PermissionIds.ToList().ForEach(x =>
                    {
                        _rolePermissionDao.Insert(_apdbContext, new RolePermission
                        {
                            PermissionId = x,
                            RoleId = entity.RoleId,
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
