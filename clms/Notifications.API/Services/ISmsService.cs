using Notifications.API.Models;

namespace Notifications.API.Services
{
    public interface ISmsService
    {
        void send(Sms sms);
    }
}
