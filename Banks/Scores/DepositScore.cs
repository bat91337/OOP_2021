using System;
using Banks.Tools;

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
            decimal scoreMoneyInScore = ScoreMoney - money;
            if (scoreMoneyInScore >= Limit)
            {
                ScoreMoney -= money;
            }
            else
            {
                throw new BanksException("you cannot raise money");
            }
        }

        public override void PutMoney(decimal money)
        {
            if (money <= 0)
            {
                ScoreMoney += money;
            }
        }

        public override void ChargePercent(int days)
        {
            decimal commissionMonth = ScoreMoney * Percent * days;
            ScoreMoney += commissionMonth;
        }
    }
}