using Courses.API.Models;

namespace Courses.API.Repository.Read
{
    public interface IReadCourseRepository : IReadRepository<Course>
    {
        Course GetByName(string name);
    }
}
