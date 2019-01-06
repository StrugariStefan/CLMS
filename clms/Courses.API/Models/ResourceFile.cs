using System;

namespace Courses.API.Models
{
    public enum ResourceType { Course, Lab}

    public class ResourceFile
    {
        public ResourceFile()
        {
            // EF
        }

        public ResourceFile(string name, string description, ResourceType type, Guid courseId)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            UploadedAt = DateTime.Now;
            Type = type;
            CourseId = courseId;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime UploadedAt { get; private set; }
        public ResourceType Type { get; private set; }
        public Guid CourseId { get; private set; }

        public virtual Course Course { get; private set; }
    }
}
