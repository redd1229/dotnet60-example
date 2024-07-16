using dotnet60_example.Helpers.Extensions;
using dotnet60_example.Models;

namespace dotnet60_example.Helpers
{
    public class SessionHelper
    {
        private const string _userSession = "UserSession";
        private readonly ISession _session;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public UserSession GetUserSession()
        {
            return _session.GetObject<UserSession>(_userSession);
        }

        public void SetUserSession(UserSession user)
        {
            _session.SetObject(_userSession, user);
        }

        public void RemoveUserSession()
        {
            RemoveSession(_userSession);
        }

        public byte[]? GetSession(string key)
        {
            return _session.Get(key);
        }

        public void SetSession(string key, byte[] value)
        {
            _session.Set(key, value);
        }

        public void RemoveSession(string key)
        {
            _session.Remove(key);
        }
    }
}
