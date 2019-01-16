using System;
using System.Collections.Generic;
using System.Linq;
using Courses.API.Context;
using Courses.API.Models;

namespace Courses.API.Repository.Read
{
    public class ReadCourseRepository : IReadCourseRepository
    {
        private readonly ApplicationContext _context;

        public ReadCourseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course GetByName(string name)
        {
            return _context.Courses.FirstOrDefault(c => c.Name == name);
        }

        public Guid GetOwnerById(Guid id)
        {
            return this._context.Courses.FirstOrDefault(c => c.Id == id).CreatedBy;
        }

        public Course GetById(Guid id)
        {
            return _context.Courses.FirstOrDefault(c => c.Id == id);
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
