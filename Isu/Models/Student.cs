using System;
namespace Isu.Properties
{
    public class Student
    {
        private static int _studentCounter;
        public Student(string name)
        {
            _studentCounter++;
            Id = _studentCounter;
        }

        public Student(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string NameStudents { get; set; }
    }
}