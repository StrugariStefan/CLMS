using System;
using System.Linq;
using Users.API.Context;
using Users.API.Models;


namespace Users.API.Repository.Write
{
    public class WriteStudentRepository : IWriteRepository<Student>
    {
        private readonly ApplicationContext _context;

        public WriteStudentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(Student student)
        {
            _context.Students.Add(student);
        }

        public void Delete(Guid id)
        {
            Student student = _context.Students.First(p => p.Id == id);
            _context.Students.Remove(student);
        }

        public bool Exists(Guid id)
        {
            return _context.Students.Any(p => p.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
