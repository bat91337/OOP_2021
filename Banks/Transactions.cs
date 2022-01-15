using System;

namespace Banks
{
    public class Transactions
    {
        public Transactions(string numberScoreSender, decimal sum, string numberScoreBeneficiary = "")
        {
            Id = Guid.NewGuid().ToString();
            NumberScoreSender = numberScoreSender;
            NumberScoreBeneficiary = numberScoreBeneficiary;
            Sum = sum;
        }

        public string Id { get; }
        public string NumberScoreSender { get; }
        public string NumberScoreBeneficiary { get; }
        public decimal Sum { get; }
    }
}