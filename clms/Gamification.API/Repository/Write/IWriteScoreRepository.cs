using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.API.Models;

namespace Gamification.API.Repository.Write
{
    public interface IWriteScoreRepository : IWriteRepository<Score>
    {
        bool Exists(Guid userId, Guid courseId);
        void Delete(Guid userId, Guid courseId);
        void AddPoints(Guid userId, Guid courseId, double points);
    }
}
