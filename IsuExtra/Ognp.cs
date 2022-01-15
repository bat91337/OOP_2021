using System.Collections.Generic;
using Isu.Properties;

namespace IsuExtra
{
    public class Ognp
    {
        public Ognp(string nameOgnp)
        {
            NameOgnp = nameOgnp;
            Students = new List<Student>();
            Disciplines = new List<Discipline>();
        }

        public string NameOgnp { get; }
        public List<Student> Students { get; }

        public List<Discipline> Disciplines { get; }
    }
}