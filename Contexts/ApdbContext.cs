using dotnet60_example.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet60_example.Contexts
{
    public class ApdbContext : DbContext
    {
        public ApdbContext(DbContextOptions<ApdbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                        .HasMany(x => x.Roles)
                        .WithMany(x => x.Accounts)
                        .UsingEntity<AccountRole>();

            modelBuilder.Entity<Role>()
                        .HasMany(x => x.Permissions)
                        .WithMany(x => x.Roles)
                        .UsingEntity<RolePermission>();
        }

        #region 主資料表
        public DbSet<Account> Account { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<AccountRole> AccountRole { get; set; }

        public DbSet<RolePermission> RolePermission { get; set; }

        public DbSet<CenterFileRectangle> CenterFileRectangle { get; set; }
        #endregion
    }
}
