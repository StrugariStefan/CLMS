using System;
using System.Collections.Generic;
using Users.API.Models;

namespace Users.API.Repository.Read
{
    public class ReadStudentRepository: IReadRepository<Student>
    {
        public Student GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Student> GetAll()
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
