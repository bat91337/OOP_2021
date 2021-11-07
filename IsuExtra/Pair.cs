using Isu.Tools;

namespace IsuExtra
{
    public class Pair
    {
        public Pair(int number, string pairName, string classRoom)
        {
            if (string.IsNullOrWhiteSpace(pairName))
            {
                throw new IsuException("empty line");
            }

            PairName = pairName;
            PairNumber = number;
            ClassRoom = classRoom;
        }

        public int PairNumber { get; }
        public string PairName { get; }
        public string ClassRoom { get; }
    }
}