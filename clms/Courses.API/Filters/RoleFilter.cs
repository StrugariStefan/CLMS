namespace Courses.API.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Newtonsoft.Json;

    public class RoleFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(
            ActionExecutingContext filterContext,
            ActionExecutionDelegate next)
        {
            var headers = filterContext.HttpContext.Request.Headers["AuthToken"];
            var token = headers.FirstOrDefault();
            var resultContext = await IsUserRoleValidAsync(token);

            if (!resultContext)
            {
                filterContext.Result = new BadRequestObjectResult("Unauthorized user!");
            }
            else
            {
                await next();
            }
        }

        private static async Task<bool> IsUserRoleValidAsync(string token)
        {
            var authUri = $"http://localhost:5003/api/v1/auth/loggedIn/{token}";
            var authResponse = new HttpClient().GetAsync(authUri).Result;

            if (authResponse.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            var authContent = authResponse.Content.ReadAsStringAsync().Result;
            var userId = JsonConvert.DeserializeObject<string>(authContent);
            using (var client = new HttpClient())
            {
                var userUri = $"http://localhost:5001/api/v1/users/{userId}";
                client.DefaultRequestHeaders.Add("AuthToken", token);
                var userResponse = await client.GetStringAsync(userUri);

                var userRole = JsonConvert.DeserializeObject<Dictionary<string, string>>(userResponse);

                if (userRole.TryGetValue("role", out var role) && int.Parse(role) == 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
