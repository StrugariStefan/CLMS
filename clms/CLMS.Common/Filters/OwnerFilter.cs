namespace CLMS.Common
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Newtonsoft.Json;

    public class OwnerFilter : ActionFilterAttribute
    {
        public static void GetProperty(ActionExecutingContext filterContext, string propertyName, out object propertyValue)
        {
            var requestItems = filterContext.HttpContext.Items;
            if (requestItems.ContainsKey(propertyName))
            {
                propertyValue = requestItems[propertyName];
                return;
            }

            if (filterContext.ActionArguments.ContainsKey(propertyName))
            {
                propertyValue = filterContext.ActionArguments[propertyName];
                return;
            }

            propertyValue = string.Empty;
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext filterContext,
            ActionExecutionDelegate next)
        {

            var resultContext = await IsUserOwnershipValidAsync(filterContext);

            if (!resultContext)
            {
                filterContext.Result = new BadRequestObjectResult("Unauthorized user - owner privileges!");
            }
            else
            {
                await next();
            }
        }

        private static async Task<bool> IsUserOwnershipValidAsync(ActionExecutingContext filterContext)
        {
            GetProperty(filterContext, "UserId", out var userId);
            GetProperty(filterContext, "AuthToken", out var token);
            GetProperty(filterContext, "id", out var courseId);

            using (var client = new HttpClient())
            {
                var courseUri = $"http://localhost:5004/api/v1/courses/{courseId}";
                client.DefaultRequestHeaders.Add("AuthToken", token.ToString());

                var checkResponse = client.GetAsync(courseUri).Result;
                if (checkResponse.StatusCode != HttpStatusCode.OK)
                {
                    return true;
                }

                var courseResponse = await client.GetStringAsync(courseUri);
                var course = JsonConvert.DeserializeObject<Dictionary<string, string>>(courseResponse);

                return course.TryGetValue("createdBy", out var owner) && owner.Equals(userId);
            }
        }
    }
}
