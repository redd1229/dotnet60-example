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
    public class PermissionServiceImpl : IPermissionService
    {
        private readonly IMapper _mapper;
        private readonly IPermissionDao _permissionDao;
        private readonly ApdbContext _apdbContext;

        public PermissionServiceImpl(IMapper mapper,
                                     IPermissionDao permissionDao,
                                     ApdbContext apdbContext)
        {
            _mapper = mapper;
            _permissionDao = permissionDao;
            _apdbContext = apdbContext;
        }

        public IList<PermissionVM> SearchPermission(PermissionSearchVM searchVM)
        {
            var entity = _mapper.Map<Permission>(searchVM);
            var list = _permissionDao.GetByEntity(_apdbContext, entity)
                                     .OrderBy(x => x.ControllerName)
                                     .ThenBy(x => x.ActionName);
            return _mapper.Map<IList<PermissionVM>>(list);
        }

        public ResultVM GetPermission(int permissionId)
        {
            var entity = _permissionDao.GetById(_apdbContext, permissionId)
                                       .FirstOrDefault();
            if (entity is null)
            {
                return new ResultVM
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = Resources.Language.PermissionNotFound
                };
            }
            return new ResultVM
            {
                StatusCode = HttpStatusCode.OK,
                Message = Resources.Language.Success,
                Body = _mapper.Map<PermissionVM>(entity)
            };
        }

        public ResultVM CreatePermission(PermissionCreateVM createVM)
        {
            using var tran = _apdbContext.Database.BeginTransaction();
            try
            {
                var entity = _mapper.Map<Permission>(createVM);
                _permissionDao.Insert(_apdbContext, entity);

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

        public ResultVM DeletePermission(int permissionId)
        {
            using var tran = _apdbContext.Database.BeginTransaction();
            try
            {
                //Include RolePermissions Cascade Delete
                var entity = _permissionDao.GetById(_apdbContext, permissionId)
                                           .Include(x => x.RolePermissions)
                                           .FirstOrDefault();
                if (entity is not null)
                {
                    _permissionDao.Delete(_apdbContext, entity);
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

        public ResultVM EditPermission(PermissionEditVM editVM)
        {
            using var tran = _apdbContext.Database.BeginTransaction();
            try
            {
                var entity = _permissionDao.GetById(_apdbContext, editVM.PermissionId)
                                           .FirstOrDefault();
                if (entity is not null)
                {
                    _mapper.Map(editVM, entity);
                    _permissionDao.Update(_apdbContext, entity);
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
