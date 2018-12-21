using System;
using System.Collections.Generic;
using Users.API.Models;

namespace Users.API.Repository.Read
{
    public interface IReadRepository
    {
        User GetById(Guid id);
        IReadOnlyList<User> GetAll();
        User GetByEmail(string email);
        IReadOnlyList<User> GetByRole(int role);
        bool Exists(Guid id);
        void SaveChanges();
    }
}