using System.Collections.Generic;
using Courses.API.Models;

namespace Courses.API.Repository.Read
{
    public interface IReadResourceFileRepository : IReadRepository<ResourceFile>
    {
        IReadOnlyList<ResourceFile> GetByType(ResourceType type);
    }
}
