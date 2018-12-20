using System;

namespace Users.API.Repository.Write
{
    public interface IWriteRepository<T>
    {
        void Create(T user);
        void Delete(Guid id);
        bool Exists(Guid id);
        void SaveChanges();
    }
}
