using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.API.Models
{
    public class StudentDto : UserDto
    {
        //public IEnumerable<String> EnrolledClasses { get; set; }
        //public IEnumerable<int> Grades { get; set; }
        public int Year { get; set; }
        public string StudyProgram { get; set; }
    }
}
