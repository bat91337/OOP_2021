using System;

namespace Banks.Scores
{
    public class CreditScore : BankAccount
    {
        public CreditScore(decimal scoreMoney, decimal percent, decimal limit, Client client, DateTime dateTime)
            : base(scoreMoney, percent, limit, client, dateTime)
        {
        }

        public override void RaiseMoney(decimal money)
        {
            if (ScoreMoney > money)
            {
                ScoreMoney -= money;
            }
            else
            {
                Console.WriteLine("no money");
            }
        }

        public override void PutMoney(decimal money)
        {
            ScoreMoney += money;
        }

        public override void ChargePercent(DateTime dateTime, DateTime dateTime1)
        {
            TimeSpan dayMonth = dateTime.Subtract(dateTime1);
            if (ScoreMoney < Limit)
            {
                if (dayMonth.Days >= 28)
                {
                    decimal commission = Limit - ScoreMoney;
                    decimal commissionMonth = commission * Percent * 30;
                    ScoreMoney -= commissionMonth;
                }
            }
        }
    }
}