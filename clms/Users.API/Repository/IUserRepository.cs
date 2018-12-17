using System;
using System.Collections.Generic;
using Users.API.Models;

namespace Users.API.Repository
{
    public interface IUserRepository
    {
        void Create(User user);
        void Delete(Guid id);
        User GetById(Guid id);
        IReadOnlyList<User> GetAll();
        bool Exists(Guid id);
        void SaveChanges();
    }
}