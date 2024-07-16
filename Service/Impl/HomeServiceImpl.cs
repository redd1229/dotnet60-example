using dotnet60_example.Contexts;
using dotnet60_example.Dao.Interface;
using dotnet60_example.Entities;
using dotnet60_example.Service.Interface;

namespace dotnet60_example.Service.Impl
{
    public class HomeServiceImpl : IHomeService
    {
        private readonly IHomeDao _homeDao;
        private readonly ApdbContext _apdbContext;

        public HomeServiceImpl(IHomeDao homeDao,
                               ApdbContext apdbContext)
        {
            _homeDao = homeDao;
            _apdbContext = apdbContext;
        }

        public IList<CenterFileRectangle> GetRectangle()
        {
            return _homeDao.GetRectangle(_apdbContext).ToList();
        }
    }
}
