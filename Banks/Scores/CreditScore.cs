using System;
using Banks.Tools;

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
                throw new BanksException("no money");
            }
        }

        public override void PutMoney(decimal money)
        {
            ScoreMoney += money;
        }

        public override void ChargePercent(int days)
        {
            decimal commission = Limit - ScoreMoney;
            if (commission > 0)
            {
                decimal commissionMonth = commission * Percent * days;
                ScoreMoney -= commissionMonth;
            }
        }
    }
}