using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.API.Models
{
    public class ScoreDto
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public double ActualScore { get; set; }
    }
}
