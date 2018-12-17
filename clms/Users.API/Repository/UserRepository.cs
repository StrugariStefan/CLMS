using System;
using System.Collections.Generic;
using System.Linq;
using Users.API.Models;

namespace Users.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
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
