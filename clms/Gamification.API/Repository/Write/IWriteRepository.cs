using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamification.API.Repository.Write
{
    public interface IWriteRepository<T>
    {
        void Create(T entity);
        void Delete(Guid id);
        bool Exists(Guid id);
        void SaveChanges();
    }
}
