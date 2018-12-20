using System;
using System.Collections.Generic;
using System.Linq;
using Users.API.Context;
using Users.API.Models;

namespace Users.API.Repository.Read
{
    public class ReadUserRepository : IReadRepository<User>
    {
        private readonly ApplicationContext _context;

        public ReadUserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public User GetById(Guid id)
        {
            return _context.Users.First(p => p.Id == id);
        }

        public IReadOnlyList<User> GetAll()
        {
            return _context.Users.ToList();
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
