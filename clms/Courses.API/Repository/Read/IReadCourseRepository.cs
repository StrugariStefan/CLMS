using Courses.API.Models;

namespace Courses.API.Repository.Read
{
    using System;

    public interface IReadCourseRepository : IReadRepository<Course>
    {
        Course GetByName(string name);
        Guid GetOwnerById(Guid id);
    }
}
