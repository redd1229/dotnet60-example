using dotnet60_example.Contexts;
using dotnet60_example.Entities;

namespace dotnet60_example.Dao.Interface
{
    public interface IHomeDao
    {
        IQueryable<CenterFileRectangle> GetRectangle(ApdbContext context);
    }
}
