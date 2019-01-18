namespace CLMS.Common.Filters
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Newtonsoft.Json;

    // https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var filterProperty = new ActionFilterProperty(filterContext, true);
            var headers = filterContext.HttpContext.Request.Headers["AuthToken"];
            var token = headers.FirstOrDefault();

            if (token != null)
            {
                filterProperty.Set("AuthToken", token);
                if (IsAuthTokenValid(filterProperty))
                {
                    return;
                }
            }

            filterContext.Result = new UnauthorizedResult();
        }

        private static bool IsAuthTokenValid(ActionFilterProperty filterProperty)
        {
            filterProperty.Get("AuthToken", out var token);
            var uri = $"http://localhost:5003/api/v1/auth/loggedIn/{token}";
            var authResponse = new HttpClient().GetAsync(uri).Result;

            if (authResponse.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            var authContent = authResponse.Content.ReadAsStringAsync().Result;
            var userId = JsonConvert.DeserializeObject<string>(authContent);
            filterProperty.Set("UserId", userId);
            return true;
        }
    }
}
