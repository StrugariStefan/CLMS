using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.API.Context;
using Gamification.API.Models;

namespace Gamification.API.Repository.Read
{
    public class ReadScoreRepository : IReadScoreRepository
    {
        private readonly ApplicationContext _context;

        public ReadScoreRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Score GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Score GetByIds(Guid userId, Guid courseId)
        {
            return _context.Scores.FirstOrDefault(s => s.UserId == userId && s.CourseId == courseId);
        }

        public IReadOnlyList<Score> GetByUserId(Guid userId)
        {
            return _context.Scores.Where(s => s.UserId == userId).ToList();
        }

        public IReadOnlyList<Score> GetByCourseId(Guid courseId)
        {
            return _context.Scores.Where(s => s.CourseId == courseId).ToList();
        }

        public bool Exists(Guid userId, Guid courseId)
        {
            return _context.Scores.Any(s => s.UserId == userId && s.CourseId == courseId);
        }

        public IReadOnlyList<Score> GetAll()
        {
            return _context.Scores.ToList();
        }

        public bool Exists(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
