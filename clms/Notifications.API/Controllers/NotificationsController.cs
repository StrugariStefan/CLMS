using System;
using Microsoft.AspNetCore.Mvc;
using Notifications.API.Helpers;
using Notifications.API.Models;
using Notifications.API.Models.Dtos;
using Notifications.API.Models.Entities;

namespace Notifications.API.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {

        /// <summary>
        /// Sends an e-mail.
        /// </summary>  
        [HttpPost("email")]
        public ActionResult SendEmail([FromBody] EmailDto emailDto)
        {
            if (emailDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

//            _emailService.sendEmail(Email);

            return Ok();
        }

        /// <summary>
        /// Sends an sms.
        /// </summary>  
        [HttpPost("sms")]
        public ActionResult SendSms([FromBody] SmsDto smsDto)
        {
            if (smsDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

//            _smsService.sendSms(Sms);

            return Ok();
        }
    }
}