using System.Collections.Generic;
using Courses.API.Models;

namespace Courses.API.Repository.Read
{
    using System;

    public interface IReadResourceFileRepository : IReadRepository<ResourceFile>
    {
        IReadOnlyList<ResourceFile> GetByType(ResourceType type);
        Guid GetCourseById(Guid id);
        bool ExistsCourse(Guid courseId);

    }
}
