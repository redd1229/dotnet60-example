using dotnet60_example.Entities;

namespace dotnet60_example.Service.Interface
{
    public interface IHomeService
    {
        IList<CenterFileRectangle> GetRectangle();
    }
}
