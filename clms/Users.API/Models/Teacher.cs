using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.API.Models
{
    public class Teacher :  User
    {
        public Teacher(string name, string email, string phone, string password, int role) : base(name, email, phone, password, role)
        {
            //TaughtCourses = new List<String>();
        }

        //public List<String> TaughtCourses { get; private set; }
    }
}
