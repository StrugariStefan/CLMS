using Notifications.API.Models;

namespace Notifications.API.Services
{
    public interface ISmsService
    {
        void Send(Sms sms);
    }
}
