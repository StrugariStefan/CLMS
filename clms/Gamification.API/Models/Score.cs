using System;

namespace Gamification.API.Models
{
    public class Score
    {
        public Score()
        {
            // EF
        }

        public Score(Guid courseId, Guid userId)
        {
            CourseId = courseId;
            UserId = userId;
            ActualScore = 0;
        }

        public void AddPoints(double points)
        {
            ActualScore += points;
        }

        public Guid CourseId { get; private set; }
        public Guid UserId { get; private set; }
        public double ActualScore { get; private set; }
    }
}
