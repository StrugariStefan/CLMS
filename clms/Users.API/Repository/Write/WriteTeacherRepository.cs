using System;
using Users.API.Models;
using System.Linq;
using Users.API.Context;

namespace Users.API.Repository.Write
{
    public class WriteTeacherRepository : IWriteRepository<Teacher>
    {
        private readonly ApplicationContext _context;

        public WriteTeacherRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Create(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
        }

        public void Delete(Guid id)
        {
            Teacher teacher = _context.Teachers.First(p => p.Id == id);
            _context.Teachers.Remove(teacher);
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
