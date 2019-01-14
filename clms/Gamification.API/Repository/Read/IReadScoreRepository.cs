using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.API.Models;

namespace Gamification.API.Repository.Read
{
    public interface IReadScoreRepository : IReadRepository<Score>
    {
        Score GetByIds(Guid userId, Guid courseId);
        IReadOnlyList<Score> GetByUserId(Guid userId);
        IReadOnlyList<Score> GetByCourseId(Guid courseId);
        bool Exists(Guid userId, Guid courseId);
    }
}
