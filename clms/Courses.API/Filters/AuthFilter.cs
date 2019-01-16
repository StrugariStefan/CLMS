namespace Courses.API.Filters
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Primitives;

    using Newtonsoft.Json;

    // https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs
    public class AuthFilter : ActionFilterAttribute
    {
        public static void AddProperty(ActionExecutingContext filterContext, string propertyName, object propertyValue)
        {
            var requestItems = filterContext.HttpContext.Items;
            if (requestItems.ContainsKey(propertyName))
            {
                requestItems[propertyName] = propertyValue;
                return;
            }

            requestItems.Add(propertyName, propertyValue);
        }

        // OnActionExecuting – This method is called before a controller action is executed.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            StringValues headers = filterContext.HttpContext.Request.Headers["AuthToken"];
            var token = headers.FirstOrDefault();

            if (token == null)
            {
                filterContext.Result = new UnauthorizedResult();
            }

            bool isAuth = IsAuthTokenValid(token, out var userId);

            AddProperty(filterContext, "AuthToken", token);
            AddProperty(filterContext, "UserId", userId);

            if (!isAuth)
            {
                filterContext.Result = new UnauthorizedResult();
            }
        }

        private static bool IsAuthTokenValid(string token, out string userId)
        {
            var uri = $"http://localhost:5003/api/v1/auth/loggedIn/{token}";
            var authResponse = new HttpClient().GetAsync(uri).Result;

            if (authResponse.StatusCode == HttpStatusCode.OK)
            {
                var authContent = authResponse.Content.ReadAsStringAsync().Result;
                userId = JsonConvert.DeserializeObject<string>(authContent);
                return true;
            }

            userId = string.Empty;
            return false;
        }
    }
}
