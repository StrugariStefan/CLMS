using Auth.API.Models;

namespace Auth.API.Repository
{
    public interface IAuthRepository
    {
        bool IsLoggedIn(string token);
        string Login(LoginRequest loginRequest);
        void Logout(LogoutRequest logoutRequest);
    }
}
