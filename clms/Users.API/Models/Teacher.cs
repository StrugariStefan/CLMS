using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.API.Models
{
    public class Teacher :  User
    {
        public Teacher(string name, string email, string phone) : base(name, email, phone)
        {
        }
    }
}
