using System;

namespace Courses.API.Repository.Write
{
    public interface IWriteRepository<T>
    {
        void Create(T entity);
        void Delete(Guid id);
        bool Exists(Guid id);
        void SaveChanges();
    }
}
