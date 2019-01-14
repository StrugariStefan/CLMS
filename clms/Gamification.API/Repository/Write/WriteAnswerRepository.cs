using System;
using System.Linq;
using Gamification.API.Models;
using Gamification.API.Context;

namespace Gamification.API.Repository.Write
{
    public class WriteAnswerRepository : IWriteAnswerRepository
    {
        private readonly ApplicationContext _context;

        public WriteAnswerRepository(ApplicationContext context)
        {
            _context = context;
        }


        public void Create(Answer entity)
        {
            _context.Answers.Add(entity);
        }

        public void Delete(Guid id)
        {
            Answer answer = _context.Answers.First(q => q.Id == id);
            _context.Remove(answer);
        }

        public bool Exists(Guid id)
        {
            return _context.Answers.Any(q => q.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
