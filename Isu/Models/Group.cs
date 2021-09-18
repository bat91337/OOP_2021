using System;
using System.Collections.Generic;
using Isu.Properties;
using Isu.Tools;

namespace Isu.Models
{
    public class Group
    {
        private List<Student> _liststudent = new List<Student>();
        public Group(string name)
        {
            NameGroup = name;
            int startIndex = 2;
            int length = 1;
            int coursenumber = int.Parse(name.Substring(startIndex, length));
            CourseNumber = (CourseNumber)coursenumber;
        }

        public string NameGroup { get; set; }
        public List<Student> Students => _liststudent;

        public CourseNumber CourseNumber { get; set; }

        public bool CheckGroup(string name)
        {
            NameGroup = name;
            int startIndex = 0;
            int length = 1;
            string coursenumber = name.Substring(startIndex, length);
            int startIndex1 = 1;
            int length1 = 1;
            int coursenumber1 = int.Parse(name.Substring(startIndex1, length1));
            int startIndex2 = 2;
            int length2 = 1;
            int coursenumber2 = int.Parse(name.Substring(startIndex2, length2));
            int startIndex3 = 3;
            int length3 = 2;

            if (name.Length != 5)
            {
                throw new IsuException("error");
            }
            else if (coursenumber != "M")
            {
                throw new IsuException("error");
            }
            else if (coursenumber1 != 3)
            {
                throw new IsuException("error");
            }
            else if (coursenumber2 != (int)CourseNumber)
            {
                throw new IsuException("error");
            }
            else if (!int.TryParse(name.Substring(startIndex3, length3), out int coursenumber3))
            {
                throw new IsuException("error");
            }

            return true;
        }
    }
}