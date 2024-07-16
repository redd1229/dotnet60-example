using dotnet60_example.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet60_example.Contexts
{
    public class HrdbContext : DbContext
    {
        public HrdbContext(DbContextOptions<HrdbContext> options) : base(options)
        {
        }

        #region 主資料表
        public DbSet<EmpData> EmpData { get; set; }
        #endregion
    }
}
