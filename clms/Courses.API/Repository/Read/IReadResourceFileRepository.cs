using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.API.Models;

namespace Courses.API.Repository.Read
{
    public interface IReadResourceFileRepository : IReadRepository<ResourceFile>
    {
        IReadOnlyList<ResourceFile> GetByType(ResourceType type);
    }
}
