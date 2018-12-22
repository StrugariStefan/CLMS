using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Primitives;
using RestSharp;

namespace Notifications.API.Filters
{
    // https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs
    public class AuthFilter : ActionFilterAttribute
    {
        // OnActionExecuting – This method is called before a controller action is executed.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            StringValues headers = filterContext.HttpContext.Request.Headers["AuthToken"];
            if (headers.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            string token = headers.First();

            if (!IsAuthTokenValid(token))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        private bool IsAuthTokenValid(string token)
        {
            string uri = $"http://localhost:5003/api/v1/auth/tokens/{token}";

            RestRequest request = new RestRequest();
            request.Method = Method.POST;

            return new HttpClient().GetAsync(uri).Result.StatusCode == HttpStatusCode.OK;
        }
    }
}