using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.API.Context;
using Gamification.API.Models;

namespace Gamification.API.Repository.Read
{
    public class ReadAnswerRepository : IReadAnswerRepository
    {
        private readonly ApplicationContext _context;

        public ReadAnswerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Answer GetById(Guid id)
        {
            return _context.Answers.FirstOrDefault(q => q.Id == id);
        }

        public IReadOnlyList<Answer> GetAll()
        {
            return _context.Answers.ToList();
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
