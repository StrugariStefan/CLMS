using System;

namespace Gamification.API.Models
{
    public class Answer
    {
        public Answer()
        {
            // EF
        }

        public Answer(string actualAnswer, Guid createdBy, Guid questionId)
        {
            Id = Guid.NewGuid();
            ActualAnswer = actualAnswer ?? throw new ArgumentNullException(nameof(actualAnswer));
            CreatedBy = createdBy;
            QuestionId = questionId;
        }

        public Guid Id { get; private set; }
        public string ActualAnswer { get; private set; }
        public Guid CreatedBy { get; private set; }
        public virtual Guid QuestionId { get; private set; }
    }
}
