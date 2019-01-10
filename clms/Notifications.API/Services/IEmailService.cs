using Notifications.API.Models;

namespace Notifications.API.Services
{
    public interface IEmailService
    {
        void Send(Email email);
    }
}
