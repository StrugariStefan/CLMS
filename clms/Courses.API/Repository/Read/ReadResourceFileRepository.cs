using System;
using System.Collections.Generic;
using System.Linq;
using Courses.API.Context;
using Courses.API.Models;

namespace Courses.API.Repository.Read
{
    public class ReadResourceFileRepository : IReadResourceFileRepository
    {
        private readonly ApplicationContext _context;

        public ReadResourceFileRepository(ApplicationContext context)
        {
            _context = context;
        }

        public ResourceFile GetById(Guid id)
        {
            return _context.ResourceFiles.FirstOrDefault(r => r.Id == id);
        }

        public Guid GetCourseById(Guid id)
        {
            return _context.ResourceFiles.FirstOrDefault(r => r.Id == id).CourseId;
        }

        public IReadOnlyList<ResourceFile> GetAll()
        {
            return _context.ResourceFiles.ToList();
        }

        public bool Exists(Guid id)
        {
            return _context.ResourceFiles.Any(r => r.Id == id);
        }

        public bool ExistsCourse(Guid id)
        {
            var courseId = _context.ResourceFiles.FirstOrDefault(r => r.Id == id).CourseId;
            return _context.Courses.Any(c => c.Id == courseId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IReadOnlyList<ResourceFile> GetByType(ResourceType type)
        {
            return _context.ResourceFiles.Where(r => r.Type == type).ToList();
        }
    }
}
