namespace dotnet60_example.Service.Interface
{
    public interface ILoginService
    {
        bool CheckAccount(string userId, string password);
    }
}
