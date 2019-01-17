namespace CLMS.Common.Filters
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Filters;

    using Newtonsoft.Json;

    public class ActionFilterProperty
    {
        private static ActionExecutingContext filterContext;

        public ActionFilterProperty(ActionExecutingContext filterContext, bool fromBody)
        {
            ActionFilterProperty.filterContext = filterContext;

            var bodyRequestObject = this.ReadRequestBody();
            if (fromBody && !bodyRequestObject.Status.ToString().Contains("Fault"))
            {
                var questionCreate = JsonConvert.DeserializeObject<Dictionary<string, string>>(bodyRequestObject.Result);
                foreach (var (key, value) in questionCreate)
                {
                    filterContext.HttpContext.Items[key] = value;
                }
            }
        }

        public void Get(string propertyName, out object propertyValue)
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

        public void Set(string propertyName, object propertyValue)
        {
            var requestItems = filterContext.HttpContext.Items;
            if (requestItems.ContainsKey(propertyName))
            {
                requestItems[propertyName] = propertyValue;
                return;
            }

            requestItems.Add(propertyName, propertyValue);
        }

        public async Task<string> ReadRequestBody()
        {
            using (var reader = new StreamReader(filterContext.HttpContext.Request.Body, Encoding.UTF8))
            {
                reader.BaseStream.Position = 0;
                return await reader.ReadToEndAsync();
            }
        }

    }
}
