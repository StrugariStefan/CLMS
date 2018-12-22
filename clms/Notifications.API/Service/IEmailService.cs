using Notifications.API.Models;

namespace Notifications.API.Service
{
    public interface IEmailService
    {
        void send(Email email);
    }
}
