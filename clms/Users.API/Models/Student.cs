namespace Users.API.Models
{
    public class Student : User
    {
        public Student(string name, string email, string phone, string password, int year, string group, string studyProgram) : base(name, email, phone, password)
        {
            Year = year;
            Group = group;
            StudyProgram = studyProgram;
            //Grades = new Enumerable<int>();
            //EnrolledClasses = new Enumerable<String>();
        }

        //public IEnumerable<String> EnrolledClasses { get; private set; }
        //public IEnumerable<int> Grades { get; private set; }
        public int Year { get; private set; }
        public string Group { get; private set; }
        public string StudyProgram { get; private set; }
    }
}
