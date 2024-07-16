using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;
using dotnet60_example.Helpers;
using Microsoft.EntityFrameworkCore;

namespace dotnet60_example.Dao.Impl
{
    public class RoleDaoImpl : IRoleDao
    {
        private readonly SessionHelper _sessionHelper;

        public RoleDaoImpl(SessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
        }

        public IQueryable<Role> GetAll(ApdbContext context)
        {
            return context.Role;
        }

        public IQueryable<Role> GetById(ApdbContext context, int roleId)
        {
            return context.Role.Where(x => x.RoleId == roleId);
        }

        public IQueryable<Role> GetByEntity(ApdbContext context, Role entity)
        {
            IQueryable<Role> query = context.Role;

            if (!string.IsNullOrEmpty(entity.RoleName))
            {
                query = query.Where(x => EF.Functions.Like(x.RoleName!, $"%{entity.RoleName}%"));
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

        public void Insert(ApdbContext context, Role entity)
        {
            entity.CreateUser = _sessionHelper.GetUserSession().UserId;
            entity.CreateDate = DateTime.Now;
            context.Role.Add(entity);
            context.SaveChanges();
        }

        public void Update(ApdbContext context, Role entity)
        {
            entity.UpdateUser = _sessionHelper.GetUserSession().UserId;
            entity.UpdateDate = DateTime.Now;
            context.SaveChanges();
        }

        public void Delete(ApdbContext context, Role entity)
        {
            context.Role.Remove(entity);
            context.SaveChanges();
        }

        public void EnabledRole(ApdbContext context, Role entity)
        {
            entity.IsActive = true;
            entity.UpdateUser = _sessionHelper.GetUserSession().UserId;
            entity.UpdateDate = DateTime.Now;
            context.SaveChanges();
        }

        public void DisabledRole(ApdbContext context, Role entity)
        {
            entity.IsActive = false;
            entity.UpdateUser = _sessionHelper.GetUserSession().UserId;
            entity.UpdateDate = DateTime.Now;
            context.SaveChanges();
        }
    }
}
