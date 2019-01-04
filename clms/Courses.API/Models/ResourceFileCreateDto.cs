using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.API.Models
{
    public class ResourceFileCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ResourceType Type { get; set; }
        public Guid CourseId { get; set; }
    }
}
