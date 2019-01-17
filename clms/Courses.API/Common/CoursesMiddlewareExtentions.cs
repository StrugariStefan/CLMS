namespace Courses.API.Common
{
    using Microsoft.AspNetCore.Builder;

    public static class CoursesMiddlewareExtentions
    {
        public static IApplicationBuilder UseCoursesMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CoursesMiddleware>();
        }
    }
}
