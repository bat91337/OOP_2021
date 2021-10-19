using System.Collections.Generic;
using Isu.Properties;
using Isu.Tools;

namespace Isu.Models
{
    public class Group
    {
        private const int MinCourseNumber = 1;
        private const int MaxCourseNumber = 4;
        public Group(string name)
        {
            bool group = CheckGroup(name);
            if (!group)
            {
                throw new IsuException("error");
            }

            GroupName = name;
            int coursenumber = int.Parse(name.Substring(2, 1));
            CourseNumber = (CourseNumber)coursenumber;
            Students = new List<Student>();
        }

        public string GroupName { get; }
        public List<Student> Students { get; }
        public CourseNumber CourseNumber { get; }

        private bool CheckGroup(string name)
        {
            if (name.Length != 5)
            {
                return false;
            }

            if (!char.IsLetter(name, 0))
            {
                return false;
            }

            if (!int.TryParse(name.Substring(1, 4), out int number))
            {
                return false;
            }

            if (number < 0)
                return false;
            int courseNumber;

            if (!int.TryParse(name.Substring(2, 1), out courseNumber))
            {
                return false;
            }

            if (courseNumber < MinCourseNumber || courseNumber > MaxCourseNumber)
                return false;

            return true;
        }
    }
}