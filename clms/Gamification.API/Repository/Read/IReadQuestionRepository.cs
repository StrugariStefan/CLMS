using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.API.Models;

namespace Gamification.API.Repository.Read
{
    public interface IReadQuestionRepository : IReadRepository<Question>
    {
        IReadOnlyList<Question> GetByType(Models.Type type);
        IReadOnlyList<Question> GetByLevelOfInterest(LevelOfInterest levelOfInterest);
    }
}
