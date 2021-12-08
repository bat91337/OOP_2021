using System;
using System.Collections.Generic;
using Banks.Scores;

namespace Banks
{
    public class CentralBank : IObservable
    {
        private static CentralBank _instance;
        private List<Bank> _banks;
        public CentralBank()
        {
            _banks = new List<Bank>();
            CurrentDate = DateTime.Now;
            Observers = new List<IObserver>();
        }

        public DateTime CurrentDate { get; private set; }
        public List<IObserver> Observers { get; }
        public static CentralBank GetInstance(string name, decimal percentDebitScore, decimal percentCreditScore, decimal percentDepositScore)
        {
            if (_instance == null)
                _instance = new CentralBank();
            return _instance;
        }

        public void AddObserver(IObserver iObserver)
        {
            Observers.Add(iObserver);
        }

        public void RemoveObserver(IObserver iObserver)
        {
            Observers.Remove(iObserver);
        }

        public void NotifyObservers(string message)
        {
            foreach (IObserver observer in Observers)
            {
                observer.Update(message);
            }
        }

        public Bank CreateBank(string name, decimal percentDebitScore, decimal percentCreditScore, decimal limit, decimal key, decimal value)
        {
            var bank = new Bank(percentDebitScore, percentCreditScore, name, limit);
            bank.DictionaryDeposit.Add(key, value);
            _banks.Add(bank);
            AddObserver(bank);
            return bank;
        }

        public void ChangePercentDebitScore(decimal percent, DebitScore debitScore)
        {
            debitScore.Percent = percent;
            string message = "your percent has changed to" + debitScore.Percent;
            NotifyObservers(message);
        }

        public void ChangePercentDepositScore(decimal key, decimal value)
        {
            foreach (Bank bank in _banks)
            {
                bank.DictionaryDeposit = new Dictionary<decimal, decimal>();
                bank.DictionaryDeposit.Add(key, value);
            }

            string message = "your percent has changed to";
            NotifyObservers(message);
        }

        public void ChangePercentCreditScore(decimal percent, CreditScore score)
        {
            score.Percent = percent;
            string message = "your percent has changed to" + score.Percent;
            NotifyObservers(message);
        }

        public DateTime AddDays(int days)
        {
            CurrentDate = CurrentDate.AddDays(days);
            return CurrentDate;
        }
    }
}