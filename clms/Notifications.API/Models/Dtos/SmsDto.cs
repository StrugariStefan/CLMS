﻿using System.Collections.Generic;

namespace Notifications.API.Models.Dtos
{
    public class SmsDto
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Message { get; set; }
    }
}
