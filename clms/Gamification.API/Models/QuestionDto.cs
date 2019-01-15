using System;

namespace Gamification.API.Models
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid CreatedBy { get; set; }
        public string ActualQuestion { get; set; }
        public LevelOfInterest LevelOfInterest { get; set; }
        public Type Type { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
