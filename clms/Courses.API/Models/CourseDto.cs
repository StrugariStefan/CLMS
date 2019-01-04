using System;

namespace Courses.API.Models
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
