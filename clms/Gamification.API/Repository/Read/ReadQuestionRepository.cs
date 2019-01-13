using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.API.Context;
using Gamification.API.Models;
namespace Gamification.API.Repository.Read
{
    public class ReadQuestionRepository : IReadQuestionRepository
    {
        private readonly ApplicationContext _context;
        
        public ReadQuestionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Question GetById(Guid id)
        {
            return _context.Questions.FirstOrDefault(r => r.Id == id);
        }

        public IReadOnlyList<Question> GetAll()
        {
            return _context.Questions.ToList();
        }

        public bool Exists(Guid id)
        {
            return _context.Questions.Any(r => r.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IReadOnlyList<Question> GetByType(Models.Type type)
        {
            return _context.Questions.Where(r => r.Type == type).ToList();
        }
        public IReadOnlyList<Question> GetByLevelOfInterest(LevelOfInterest levelOfInterest)
        {
            return _context.Questions.Where(r => r.LevelOfInterest == levelOfInterest).ToList();
        }

    }
}
