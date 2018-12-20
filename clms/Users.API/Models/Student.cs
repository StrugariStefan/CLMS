using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.API.Models
{
    public class Student : User
    {
        public Student(string name, string email, string phone) : base(name, email, phone)
        {

        }
    }
}
