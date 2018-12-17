using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Models;

namespace Users.API.Repository.Write
{
    public interface IWriteUserRepository
    {
        void Create(User user);
        void Delete(Guid id);
        bool Exists(Guid id);
        void SaveChanges();
    }
}
