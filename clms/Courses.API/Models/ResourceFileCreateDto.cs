using System;
using Microsoft.AspNetCore.Http;

namespace Courses.API.Models
{
    public class ResourceFileCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ResourceType Type { get; set; }
        public Guid CourseId { get; set; }
        public IFormFile Resource { get; set; }
    }
}
