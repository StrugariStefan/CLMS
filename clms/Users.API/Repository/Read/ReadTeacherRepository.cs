using System;
using System.Collections.Generic;
using Users.API.Models;

namespace Users.API.Repository.Read
{
    public class ReadTeacherRepository : IReadRepository<Teacher>
    {
        public Teacher GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Teacher> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Exists(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
