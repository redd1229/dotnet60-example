using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;
using dotnet60_example.Helpers;
using Microsoft.EntityFrameworkCore;

namespace dotnet60_example.Dao.Impl
{
    public class PermissionDaoImpl : IPermissionDao
    {
        private readonly SessionHelper _sessionHelper;

        public PermissionDaoImpl(SessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
        }

        public IQueryable<Permission> GetAll(ApdbContext context)
        {
            return context.Permission;
        }

        public IQueryable<Permission> GetById(ApdbContext context, int permissionId)
        {
            return context.Permission.Where(x => x.PermissionId == permissionId);
        }

        public IQueryable<Permission> GetByEntity(ApdbContext context, Permission entity)
        {
            IQueryable<Permission> query = context.Permission;

            if (!string.IsNullOrEmpty(entity.ControllerName))
            {

                query = query.Where(x => EF.Functions.Like(x.ControllerName!, $"%{entity.ControllerName}%"));
            }

            if (!string.IsNullOrEmpty(entity.ActionName))
            {
                query = query.Where(x => EF.Functions.Like(x.ActionName!, $"%{entity.ActionName}%"));
            }

            if (!string.IsNullOrEmpty(entity.Description))
            {
                query = query.Where(x => EF.Functions.Like(x.Description!, $"%{entity.Description}%"));
            }

            if (entity.IsActive is not null)
            {
                query = query.Where(x => x.IsActive == entity.IsActive);
            }

            return query;
        }

        public void Insert(ApdbContext context, Permission entity)
        {
            entity.CreateUser = _sessionHelper.GetUserSession().UserId;
            entity.CreateDate = DateTime.Now;
            context.Permission.Add(entity);
            context.SaveChanges();
        }

        public void Update(ApdbContext context, Permission entity)
        {
            entity.UpdateUser = _sessionHelper.GetUserSession().UserId;
            entity.UpdateDate = DateTime.Now;
            context.SaveChanges();
        }

        public void Delete(ApdbContext context, Permission entity)
        {
            context.Permission.Remove(entity);
            context.SaveChanges();
        }

        public void EnabledPermission(ApdbContext context, Permission entity)
        {
            entity.UpdateUser = _sessionHelper.GetUserSession().UserId;
            entity.UpdateDate = DateTime.Now;
            entity.IsActive = true;
            context.SaveChanges();
        }

        public void DisabledPermission(ApdbContext context, Permission entity)
        {
            entity.UpdateUser = _sessionHelper.GetUserSession().UserId;
            entity.UpdateDate = DateTime.Now;
            entity.IsActive = false;
            context.SaveChanges();
        }
    }
}
