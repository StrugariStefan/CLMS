using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.API.Models;
using Gamification.API.Context;

namespace Gamification.API.Repository.Write
{
    public class WriteQuestionRepository : IWriteQuestionRepository
    {
        private readonly ApplicationContext _context;

        public WriteQuestionRepository(ApplicationContext context)
        {
            _context = context;
        }


        public void Create(Question entity)
        {
            _context.Questions.Add(entity);
        }

        public void Delete(Guid id)
        {
            Question question = _context.Questions.First(r => r.Id == id);
            _context.Remove(question);
        }

        public bool Exists(Guid id)
        {
            return _context.Questions.Any(r => r.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
