using System;
using System.Collections.Generic;
using Isu.Properties;

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
    }
}