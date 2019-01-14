using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.API.Context;
using Gamification.API.Models;

namespace Gamification.API.Repository.Write
{
    public class WriteScoreRepository : IWriteScoreRepository
    {
        private readonly ApplicationContext _context;

        public WriteScoreRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(Score entity)
        {
            _context.Scores.Add(entity);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid userId, Guid courseId)
        {
            var score = _context.Scores.First(s => s.UserId == userId && s.CourseId == courseId);
            _context.Remove(score);
        }

        public void AddPoints(Guid userId, Guid courseId, double points)
        {
            var score = _context.Scores.First(s => s.UserId == userId && s.CourseId == courseId);
            score.AddPoints(points);
        }

        public bool Exists(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Guid userId, Guid courseId)
        {
            return _context.Scores.Any(s => s.UserId == userId && s.CourseId == courseId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
