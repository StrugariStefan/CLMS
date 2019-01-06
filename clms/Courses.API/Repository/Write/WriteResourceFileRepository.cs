using System;
using System.Linq;
using Courses.API.Context;
using Courses.API.Models;

namespace Courses.API.Repository.Write
{
    public class WriteResourceFileRepository : IWriteResourceFileRepository
    {
        private readonly ApplicationContext _context;

        public WriteResourceFileRepository(ApplicationContext context)
        {
            _context = context;
        }


        public void Create(ResourceFile entity)
        {
            _context.ResourceFiles.Add(entity);
        }

        public void Delete(Guid id)
        {
            ResourceFile resourceFile = _context.ResourceFiles.First(r => r.Id == id);
            _context.Remove(resourceFile);
        }

        public bool Exists(Guid id)
        {
            return _context.ResourceFiles.Any(r => r.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
