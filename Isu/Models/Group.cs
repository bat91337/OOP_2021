using System;
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
            CheckGroup(name);
            GroupName = name;
            int coursenumber = int.Parse(name.Substring(2, 1));
            CourseNumber = (CourseNumber)coursenumber;
            Students = new List<Student>();
            bool group = CheckGroup(name);
            if (group.Equals(false))
            {
                throw new IsuException("error");
            }
        }

        public string GroupName { get; private set; }
        public List<Student> Students { get; }

        public CourseNumber CourseNumber { get; private set; }

        private bool CheckGroup(string name)
        {
            if (name.Length != 5)
            {
                return false;
            }

            if (!name.Substring(0, 2).Equals("M3"))
                return false;

            int courseNumber;

            if (!int.TryParse(name.Substring(2, 1), out courseNumber))
            {
                return false;
            }

            if (courseNumber < MinCourseNumber || courseNumber > MaxCourseNumber)
                return false;

            if (!int.TryParse(name.Substring(3, 2), out int number))
            {
                return false;
            }

            if (number < 0)
                return false;

            return true;
        }
    }
}