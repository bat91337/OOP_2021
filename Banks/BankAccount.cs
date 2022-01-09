using System;

namespace Banks
{
    public abstract class BankAccount
    {
        public BankAccount(decimal scoreMoney, decimal percent, decimal limit, Client client, DateTime dateTime)
        {
            ScoreMoney = scoreMoney;
            Percent = percent;
            Limit = limit;
            NumberScore = Guid.NewGuid().ToString();
            Client = client;
            DateTime = dateTime;
        }

        public decimal Percent { get; set; }
        public decimal ScoreMoney { get; set; }
        public decimal Limit { get; }
        public string NumberScore { get; }
        public Client Client { get; }
        public DateTime DateTime { get; }

        public abstract void RaiseMoney(decimal money);

        public abstract void PutMoney(decimal money);

        public abstract void ChargePercent(int days);
    }
}