using System;
namespace Isu.Properties
{
    public class Student
    {
        public Student(string name, int id)
        {
            NameStudents = name;
            Id = id;
        }

        public int Id { get; }

        public string NameStudents { get; }
    }
}