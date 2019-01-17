namespace Gamification.API.Models
{
    using System;
    using System.Runtime.Serialization;

    public class AnswerCreateDto
    {
        public string ActualAnswer { get; set; }
        [IgnoreDataMember]
        public Guid CreatedBy { get; set; }
        public virtual Guid QuestionId { get; set; }
    }
}