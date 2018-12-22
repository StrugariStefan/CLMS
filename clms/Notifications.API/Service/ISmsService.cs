using Notifications.API.Models;

namespace Notifications.API.Service
{
    public interface ISmsService
    {
        void send(Sms sms);
    }
}
