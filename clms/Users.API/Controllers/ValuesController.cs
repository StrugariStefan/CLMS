using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace Users.API.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "si value3 din users api" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value din users api";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
//            SmtpClient client = new SmtpClient(); //host and port picked from web.config
//            client.EnableSsl = true;
//
//            MailMessage message = new MailMessage();
//            message.IsBodyHtml = true;
//            message.From = new MailAddress("test@test.com");
//            message.Subject = "Testare pentru .NET";
//            message.To.Clear();
//            message.To.Add(new MailAddress("test@test.gmail"));
//            client.Send(message);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
