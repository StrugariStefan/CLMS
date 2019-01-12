using System;
using System.Collections.Generic;

namespace Gamification.API.Models
{
    public enum LevelOfInterest{ Public, Private }
    public enum Type { Timed, Normal}

    public class Question
    {
        public Question()
        {
            // EF
        }

        public Question(Guid courseId, Guid createdBy, string actualQuestion, LevelOfInterest levelOfInterest, Type type, decimal timeInMinutes = 0)
        {
            Id = Guid.NewGuid();
            CourseId = courseId;
            CreatedBy = createdBy;
            ActualQuestion = actualQuestion ?? throw new ArgumentNullException(nameof(actualQuestion));
            LevelOfInterest = levelOfInterest;
            Type = type;
            DeadLine = ComputeDeadLine(timeInMinutes);
            Answers = new HashSet<Answer>();
        }

        private DateTime ComputeDeadLine(decimal timeInMinutes)
        {
            return DateTime.Now.AddMinutes((double)timeInMinutes);
        }

        public bool QuestionTimeFinished()
        {
            if (Type == Type.Normal)
            {
                return true;
            }

            return DateTime.Compare(DateTime.Now, DeadLine) >= 0;
        }
        
        public Guid Id { get; private set; }
        public Guid CourseId { get; private set; }
        public Guid CreatedBy { get; private set; }
        public string ActualQuestion { get; private set; }
        public LevelOfInterest LevelOfInterest { get; private set; }
        public Type Type { get; private set; }
        public DateTime DeadLine { get; private set; }

        public virtual ICollection<Answer> Answers { get; private set; }
    }
}
