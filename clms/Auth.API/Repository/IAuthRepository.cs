using System;
using Auth.API.Models;

namespace Auth.API.Repository
{
    public interface IAuthRepository
    {
        bool IsLoggedIn(string token, out Guid userId);
        string Login(LoginRequest loginRequest);
        void Logout(LogoutRequest logoutRequest);
    }
}
