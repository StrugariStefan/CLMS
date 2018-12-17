using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Notifications.API.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var client = GetHttpClient();
            return await client.GetStringAsync("http://users.api/api/values/5");
        }

        private static HttpClient GetHttpClient()
        {
            var handler = GetHttpClientHandler();
            HttpClient client = new HttpClient(handler);
            return client;
        }

        private static HttpClientHandler GetHttpClientHandler()
        {
            var handler = new HttpClientHandler();
//            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
//            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            return handler;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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