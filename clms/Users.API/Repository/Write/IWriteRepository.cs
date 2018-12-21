using System;
using Users.API.Models;

namespace Users.API.Repository.Write
{
    public interface IWriteRepository
    {
        void Create(User user);
        void Delete(Guid id);
        bool Exists(Guid id);
        void SaveChanges();
    }
}
