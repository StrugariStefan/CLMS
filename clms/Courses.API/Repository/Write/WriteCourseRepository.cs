using System;
using System.Linq;
using Courses.API.Context;
using Courses.API.Models;

namespace Courses.API.Repository.Write
{
    public class WriteCourseRepository : IWriteCourseRepository
    {
        private readonly ApplicationContext _context;

        public WriteCourseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(Course course)
        {
            _context.Courses.Add(course);
        }


        public void Delete(Guid id)
        {
            Course course = _context.Courses.First(c => c.Id == id);
            _context.Courses.Remove(course);
        }

        public bool Exists(Guid id)
        {
            return _context.Courses.Any(c => c.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
