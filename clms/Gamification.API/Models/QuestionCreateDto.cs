namespace Gamification.API.Models
{
    using System;
    using System.Runtime.Serialization;

    public class QuestionCreateDto
    {
        public Guid CourseId { get; set; }
        [IgnoreDataMember]
        public Guid CreatedBy { get; set; }
        public string ActualQuestion { get; set; }
        public LevelOfInterest LevelOfInterest { get; set; }
        public Type Type { get; set; }
        //public DateTime DeadLine { get; set; }
        public decimal TimeInMinutes { get; set; }
    }
}
