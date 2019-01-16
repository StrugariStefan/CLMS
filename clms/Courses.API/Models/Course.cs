using System;
using System.Collections.Generic;

namespace Courses.API.Models
{
    public class Course
    {
        public Course()
        {
            // EF
        }

        public Course(Guid userId, string name, string description)
        {
            Id = Guid.NewGuid();
            CreatedBy = userId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ResourceFiles = new HashSet<ResourceFile>();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid CreatedBy { get; private set; }

        public virtual ICollection<ResourceFile> ResourceFiles { get; private set; }
    }
}
