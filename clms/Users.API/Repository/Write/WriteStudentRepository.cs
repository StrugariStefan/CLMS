using System;
using Users.API.Models;

namespace Users.API.Repository.Write
{
    public class WriteStudentRepository : IWriteRepository<Student>
    {
        public void Create(Student user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
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
