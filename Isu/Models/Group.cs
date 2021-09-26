using System;
using System.Collections.Generic;
using Isu.Properties;
using Isu.Tools;

namespace Isu.Models
{
    public class Group
    {
        public Group(string name)
        {
            CheckGroup(name);
            NameGroup = name;
            int coursenumber = int.Parse(name.Substring(2, 1));
            CourseNumber = (CourseNumber)coursenumber;
            Students = new List<Student>();
        }

        public string NameGroup { get; private set; }
        public List<Student> Students { get; }

        public CourseNumber CourseNumber { get; private set; }

        private bool CheckGroup(string name)
        {
            if (name.Length != 5)
            {
                throw new IsuException("error");
            }

            if (!name.Substring(0, 2).Equals("M3"))
                throw new IsuException("wrong group name");

            int courseNumber;

            if (!int.TryParse(name.Substring(2, 1), out courseNumber))
            {
                throw new IsuException("course number must be a number");
            }

            if (courseNumber < 1 || courseNumber > 4)
                throw new IsuException("wrong course number");

            CourseNumber = (CourseNumber)courseNumber;

            if (!int.TryParse(name.Substring(3, 2), out int number))
            {
                throw new IsuException("group number must be int");
            }

            if (number < 0)
                throw new IsuException("group number must be a positive number");

            NameGroup = name;

            return true;
        }
    }
}