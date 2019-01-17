namespace CLMS.Common.Filters
{
    using System.Collections.Generic;
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
            var filterProperty = new ActionFilterProperty(filterContext, false);
            var resultContext = await IsUserRoleValidAsync(filterProperty);

            if (!resultContext)
            {
                filterContext.Result = new BadRequestObjectResult("Unauthorized user!");
            }
            else
            {
                await next();
            }
        }

        private static async Task<bool> IsUserRoleValidAsync(ActionFilterProperty filterProperty)
        {
            filterProperty.Get("UserId", out var userId);
            filterProperty.Get("AuthToken", out var token);

            using (var client = new HttpClient())
            {
                var userUri = $"http://localhost:5001/api/v1/users/{userId}";
                client.DefaultRequestHeaders.Add("AuthToken", token.ToString());

                var checkResponse = client.GetAsync(userUri).Result;
                if (checkResponse.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                var userResponse = await client.GetStringAsync(userUri);
                var userRole = JsonConvert.DeserializeObject<Dictionary<string, string>>(userResponse);

                return userRole.TryGetValue("role", out var role) && int.Parse(role) == 2;
            }
        }
    }
}
