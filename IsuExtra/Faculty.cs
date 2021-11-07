using System.Collections.Generic;
using Isu.Properties;

namespace IsuExtra
{
    public class Faculty
    {
        public Faculty(string nameFaculty, string nameOgnp, char firstLetter)
        {
            NameOgnp = nameOgnp;
            NameFaculty = nameFaculty;
            Students = new List<Student>();
            Disciplines = new List<Discipline>();
            FirstLetter = firstLetter;
        }

        public string NameOgnp { get; }
        public string NameFaculty { get; }
        public List<Student> Students { get; }
        public List<Discipline> Disciplines { get; }
        public char FirstLetter { get; }
    }
}