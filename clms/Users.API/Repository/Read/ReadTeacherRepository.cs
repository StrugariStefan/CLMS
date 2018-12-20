using System;
using System.Collections.Generic;
using Users.API.Models;
using System.Linq;
using Users.API.Context;

namespace Users.API.Repository.Read
{
    public class ReadTeacherRepository : IReadRepository<Teacher>
    {
        private readonly ApplicationContext _context;
        public ReadTeacherRepository(ApplicationContext context)
        {
            _context = context;
        }
        public Teacher GetById(Guid id)
        {
            return _context.Teachers.First(p => p.Id == id);
        }

        public IReadOnlyList<Teacher> GetAll()
        {
            return _context.Teachers.ToList();
        }

        public bool Exists(Guid id)
        {
            return _context.Teachers.Any(p => p.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
