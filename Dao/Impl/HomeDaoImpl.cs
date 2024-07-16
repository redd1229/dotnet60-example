using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;

namespace dotnet60_example.Dao.Impl
{
    public class HomeDaoImpl : IHomeDao
    {
        public IQueryable<CenterFileRectangle> GetRectangle(ApdbContext context)
        {
            return context.CenterFileRectangle.Where(x => x.Barcode == "A100")
                                              .OrderBy(x => x.PcsId);
        }
    }
}
