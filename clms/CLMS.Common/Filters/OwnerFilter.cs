namespace CLMS.Common.Filters
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
        public override async Task OnActionExecutionAsync(
            ActionExecutingContext filterContext,
            ActionExecutionDelegate next)
        {
            var filterProperty = new ActionFilterProperty(filterContext, false);
            var resultContext = await IsUserOwnershipValidAsync(filterProperty);

            if (!resultContext)
            {
                filterContext.Result = new BadRequestObjectResult("Unauthorized user - owner privileges!");
            }
            else
            {
                await next();
            }
        }

        private static async Task<bool> IsUserOwnershipValidAsync(ActionFilterProperty filterProperty)
        {
            filterProperty.Get("UserId", out var userId);
            filterProperty.Get("AuthToken", out var token);
            filterProperty.Get("courseId", out var courseId);
            filterProperty.Get("type", out var type);

            if (type.Equals("Timed"))
            {
                using (var client = new HttpClient())
                {
                    var courseUri = $"http://localhost:5004/api/v1/courses/{courseId}";
                    client.DefaultRequestHeaders.Add("AuthToken", token.ToString());

                    var checkResponse = client.GetAsync(courseUri).Result;
                    if (checkResponse.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var courseResponse = await client.GetStringAsync(courseUri);
                    var course = JsonConvert.DeserializeObject<Dictionary<string, string>>(courseResponse);

                    return course.TryGetValue("createdBy", out var owner) && owner.Equals(userId);
                }
            }

            return true;
        }
    }
}
