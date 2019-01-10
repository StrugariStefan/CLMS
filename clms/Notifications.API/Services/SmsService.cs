using Notifications.API.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Notifications.API.Services
{
    public class SmsService : ISmsService
    {
        public void Send(Sms sms)
        {
            const string accountSid = "AC088012c2dd67bad3f5f60b4f67f78065";
            const string authToken = "1a5c7b117a78d72dc3b72bdd1e354d25";
            const string from = "+48799448728";

            TwilioClient.Init(accountSid, authToken);

            foreach (var to in sms.To)
            {
                MessageResource.Create(
                    from: new Twilio.Types.PhoneNumber(from),
                    to: new Twilio.Types.PhoneNumber(to),
                    body: sms.Body
                );
            }
        }
    }
}