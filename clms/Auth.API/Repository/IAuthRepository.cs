namespace Auth.API.Repository
{
    public interface IAuthRepository
    {
        bool IsLoggedIn(string token);
        string Login(string email, string password);
    }
}
