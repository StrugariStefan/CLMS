namespace Gamification.API.Repository.Read
{
    using System;
    using System.Collections.Generic;

    using Gamification.API.Models;

    public interface IReadQuestionRepository : IReadRepository<Question>
    {
        IReadOnlyList<Question> GetByType(Models.Type type);

        Guid GetOwnerById(Guid id);

        IReadOnlyList<Question> GetByLevelOfInterest(LevelOfInterest levelOfInterest);

    }
}
