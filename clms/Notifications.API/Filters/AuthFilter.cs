using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using RestSharp;

namespace Notifications.API.Filters
{
    // https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs
    public class AuthFilter : ActionFilterAttribute
    {
        // OnActionExecuting – This method is called before a controller action is executed.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsAuthTokenValid())
            {
                Console.WriteLine("The auth token is not valid");
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Console.WriteLine("Reuqest authenticated");
        }

        private bool IsAuthTokenValid()
        {
            string uri = "http://localhost:5003/api/v1/auth/tokens/testToken";

            RestRequest request = new RestRequest();
            request.Method = Method.POST;

            return new HttpClient().GetAsync(uri).Result.StatusCode == HttpStatusCode.OK;
        }
    }
}