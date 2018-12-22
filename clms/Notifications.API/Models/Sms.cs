using System.Collections.Generic;

namespace Notifications.API.Models
{
    public class Sms
    {
        public List<string> To { get; set; }
        public string Body { get; set; }
    }
}
