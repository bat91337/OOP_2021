using System.Collections.Generic;
using Isu.Properties;

namespace IsuExtra
{
    public class Faculty
    {
        public Faculty(string nameFaculty, char firstLetter)
        {
            NameFaculty = nameFaculty;
            FirstLetter = firstLetter;
            Ognp = new List<Ognp>();
        }

        public string NameFaculty { get; }
        public List<Ognp> Ognp { get; }
        public char FirstLetter { get; }
    }
}