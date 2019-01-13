using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.API.Models
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        public string ActualAnswer { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual Guid QuestionId { get; set; }
    }
}
