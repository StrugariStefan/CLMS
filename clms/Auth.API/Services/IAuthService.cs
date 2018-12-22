using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Services
{
    public interface IAuthService
    {
        bool IsLoggedIn(string token);
        string Login(string email, string password);
    }
}
