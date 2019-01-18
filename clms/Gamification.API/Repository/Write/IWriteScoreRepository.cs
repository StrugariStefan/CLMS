using System;
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
