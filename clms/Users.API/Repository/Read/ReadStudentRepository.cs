using System;
using System.Collections.Generic;
using System.Linq;
using Users.API.Context;
using Users.API.Models;

namespace Users.API.Repository.Read
{
    public class ReadStudentRepository: IReadRepository<Student>
    {
        private readonly ApplicationContext _context;

        public ReadStudentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Student GetById(Guid id)
        {        
            return _context.Students.First(p => p.Id == id);
        }

        public IReadOnlyList<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public bool Exists(Guid id)
        {
            return _context.Students.Any(p => p.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public IReadOnlyList<Student> GetByYear(int year)
        {
            return _context.Students.Where(p => p.Year == year).ToList<Student>();
        }
        public IReadOnlyList<Student> GetByGroup(string group)
        {
            return _context.Students.Where(p => p.Group == group).ToList<Student>();
        }
    }
}
