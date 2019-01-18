namespace Courses.API.Common
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class CoursesMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILoggerFactory loggerFactory;

        public CoursesMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this.loggerFactory = loggerFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            // this.loggerFactory.AddConsole();
            // var logger = this.loggerFactory.CreateLogger("Courses Api Logger");
            // logger.LogInformation("Am intrat in middleware.");



            await this.next.Invoke(context);
        }
    }
}
