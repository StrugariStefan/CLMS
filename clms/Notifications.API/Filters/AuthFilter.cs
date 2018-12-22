using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            string token = headers.FirstOrDefault();

            if (token == null)
            {
                filterContext.Result = new UnauthorizedResult();
            }
            else if (!IsAuthTokenValid(token)) 
            {
                filterContext.Result = new UnauthorizedResult();
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