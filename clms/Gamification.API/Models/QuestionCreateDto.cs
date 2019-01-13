using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.API.Models
{
    public class QuestionCreateDto
    {
        public Guid CourseId { get; set; }
        public Guid CreatedBy { get; set; }
        public string ActualQuestion { get; set; }
        public LevelOfInterest LevelOfInterest { get; set; }
        public Type Type { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
