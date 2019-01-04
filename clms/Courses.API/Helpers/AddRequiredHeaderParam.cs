using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Courses.API.Helpers
{
    // https://stackoverflow.com/questions/41493130/web-api-how-to-add-a-header-parameter-for-all-api-in-swagger
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "AuthToken",
                In = "header",
                Type = "string",
                Default = "testToken",
                Required = false
            });
        }
    }
}
