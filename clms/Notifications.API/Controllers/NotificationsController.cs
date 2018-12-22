﻿using Microsoft.AspNetCore.Mvc;
using Notifications.API.Filters;
using Notifications.API.Models;
using Notifications.API.Services;

namespace Notifications.API.Controllers
{
    [AuthFilter]
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public NotificationsController(IEmailService emailService, ISmsService smsService)
        {
            _emailService = emailService;
            _smsService = smsService;
        }

        /// <summary>
        /// Sends an e-mail.
        /// </summary>  
        [HttpPost("email")]
        public ActionResult SendEmail([FromBody] Email email)
        {
            if (email == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _emailService.send(email);

            return Ok();
        }

        /// <summary>
        /// Sends an sms.
        /// </summary>  
        [HttpPost("sms")]
        public ActionResult SendSms([FromBody] Sms sms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _smsService.send(sms);

            return Ok();
        }
    }
}