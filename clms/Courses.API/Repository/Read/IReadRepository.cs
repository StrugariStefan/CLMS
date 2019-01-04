using System;
using System.Collections.Generic;

namespace Courses.API.Repository.Read
{
    public interface IReadRepository<T>
    {
        T GetById(Guid id);
        IReadOnlyList<T> GetAll();
        bool Exists(Guid id);
        void SaveChanges();
    }
}