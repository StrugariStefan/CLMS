using System.Collections.Generic;

namespace Notifications.API.Models
{
    public class Sms
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Body { get; set; }
    }
}
