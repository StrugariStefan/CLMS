using System;

namespace Gamification.API.Models
{
    public class ScoreDto
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public double ActualScore { get; set; }
    }
}
