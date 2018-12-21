using System;
using System.Linq;
using Users.API.Context;
using Users.API.Models;

namespace Users.API.Repository.Write
{
    public class WriteUserRepository : IWriteRepository
    {
        private readonly ApplicationContext _context;

        public WriteUserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
        }


        public void Delete(Guid id)
        {
            User user = _context.Users.First(p => p.Id == id);
            _context.Users.Remove(user);
        }

        public bool Exists(Guid id)
        {
            return _context.Users.Any(p => p.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
