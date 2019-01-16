namespace Courses.API.Models
{
    using System;
    using System.Runtime.Serialization;

    public class CourseCreateDto
    {
        [IgnoreDataMember]
        public Guid CreatedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
