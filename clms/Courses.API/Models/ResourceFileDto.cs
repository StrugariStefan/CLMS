using System;

namespace Courses.API.Models
{
    public class ResourceFileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UploadedAt { get; set; }
        public ResourceType Type { get; set; }
    }
}
