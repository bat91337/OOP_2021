using System;

namespace Banks
{
    public class Transactions
    {
        public Transactions(string numberScore, decimal sum, string numberScoreNew = "")
        {
            Id = Guid.NewGuid().ToString();
            NumberScore = numberScore;
            NumberScoreNew = numberScoreNew;
            Sum = sum;
        }

        public string Id { get; }
        public string NumberScore { get; }
        public string NumberScoreNew { get; }
        public decimal Sum { get; }
    }
}