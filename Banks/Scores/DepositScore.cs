using System;

namespace Banks.Scores
{
    public class DepositScore : BankAccount
    {
        public DepositScore(decimal scoreMoney, decimal percent, decimal limit, Client client, DateTime dateTime)
            : base(scoreMoney, percent, limit, client, dateTime)
        {
        }

        public override void RaiseMoney(decimal money)
        {
            decimal scoreMoney1 = ScoreMoney - money;
            if (scoreMoney1 >= Limit)
            {
                ScoreMoney -= money;
            }
            else
            {
                Console.WriteLine("you cannot raise money");
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
                if (dayMonth.Days <= 28)
                {
                    decimal commission = Limit - ScoreMoney;
                    decimal commissionMonth = commission * Percent * 30;
                    ScoreMoney += commissionMonth;
                }
            }
        }
    }
}