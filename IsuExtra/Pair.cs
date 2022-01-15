using Isu.Tools;

namespace IsuExtra
{
    public class Pair
    {
        public Pair(int number, string pairName, string classRoom, string groupName, string nameTeacher)
        {
            if (string.IsNullOrWhiteSpace(pairName))
            {
                throw new IsuException("empty line");
            }

            PairName = pairName;
            PairNumber = number;
            ClassRoom = classRoom;
            GroupName = groupName;
            NameTeacher = nameTeacher;
        }

        public int PairNumber { get; }
        public string PairName { get; }
        public string GroupName { get; }
        public string NameTeacher { get; }
        public string ClassRoom { get; }
    }
}