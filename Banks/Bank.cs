using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Banks.Scores;
using Banks.Tools;

namespace Banks
{
    public class Bank : IObservable, IObserver
    {
        public Bank(decimal percentDebitScore, decimal percentCreditScore, string name, decimal limit, decimal percentDepositScore)
        {
            Client = new List<Client>();
            Observers = new List<IObserver>();
            PercentDebitScore = percentDebitScore;
            PercentCreditScore = percentCreditScore;
            Message = new List<string>();
            Name = name;
            Id = Guid.NewGuid().ToString();
            Accounts = new List<Account>();
            LimitCreditScore = limit;
            CurrentDate = DateTime.Now;
            DictionaryDeposit = new Dictionary<decimal, decimal>();
            TransactionsList = new List<Transactions>();
            PercentDepositScore = percentDepositScore;
        }

        public delegate void MessageDelegate(string message);

        public event MessageDelegate NotifyObserversDelegate;

        public List<Client> Client { get; }
        public List<IObserver> Observers { get; }
        public List<string> Message { get; }
        public decimal PercentDebitScore { get; private set; }
        public decimal PercentCreditScore { get; private set; }
        public decimal PercentDepositScore { get; private set; }
        public decimal LimitCreditScore { get; }
        public string Name { get; }
        public string Id { get; }
        public List<Account> Accounts { get; }
        public DateTime CurrentDate { get; private set; }
        public Dictionary<decimal, decimal> DictionaryDeposit { get; set; }
        public List<Transactions> TransactionsList { get; }
        public void ChangePercentCreditScore(decimal newPercent)
        {
            PercentCreditScore = newPercent;
            string message = "your percent has changed to" + PercentDebitScore;
            NotifyObservers(message);
            NotifyObserversDelegate?.Invoke(message);
        }

        public void ChangePercentDebitScore(decimal newPercent)
        {
            PercentDebitScore = newPercent;
            string message = "your percent has changed to" + PercentDebitScore;
            NotifyObservers(message);
            NotifyObserversDelegate?.Invoke(message);
        }

        public void ChangePercentDepositScore(decimal newPercent)
        {
            PercentDepositScore = newPercent;
            string message = "your percent has changed to" + PercentDepositScore;
            NotifyObservers(message);
            NotifyObserversDelegate?.Invoke(message);
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

        public void Update(string str)
        {
            Message.Add(str);
        }

        public Client CreateClient(Client client)
        {
            AddObserver(client);
            Client.Add(client);
            return client;
        }

        public Account CreateAccount(Client client)
        {
            var account = new Account(client);
            Accounts.Add(account);
            return account;
        }

        public void AddAddress(string passport, string address)
        {
            foreach (Client client in Client.Where(client => client.Passport.Equals(passport)))
            {
                if (string.IsNullOrWhiteSpace(address))
                {
                    throw new BanksException("incorrect data entered");
                }

                client.Address = address;
            }
        }

        public void AddNumberPhone(string passport, string numberPhone)
        {
            foreach (Client client in Client.Where(client => client.Passport.Equals(passport)))
            {
                if (string.IsNullOrWhiteSpace(numberPhone))
                {
                    throw new BanksException("incorrect data entered");
                }

                client.NumberPhone = numberPhone;
            }
        }

        public CreditScore CreateCreditScore(string id, decimal money)
        {
            Account account = Accounts.FirstOrDefault(account => id.Equals(account.Id));

            money += LimitCreditScore;
            if (account != null)
            {
                var creditScore = new CreditScore(money, PercentCreditScore, LimitCreditScore, account.Client, DateTime.Now);
                account.Scores.Add(creditScore);
                return creditScore;
            }

            throw new BanksException("failed to create credit score");
        }

        public DebitScore CreateDebitScore(string account, decimal money)
        {
            Account account1 = Accounts.FirstOrDefault(account1 => account1.Id.Equals(account));
            const decimal limit = 0;
            if (account1 != null)
            {
                var debitScore = new DebitScore(money, PercentDebitScore, limit, account1.Client, DateTime.Now);
                account1.Scores.Add(debitScore);
                return debitScore;
            }

            throw new BanksException("failed to create debit score");
        }

        public DepositScore CreateDepositScore(string account, decimal money)
        {
            foreach (Account account1 in Accounts)
            {
                if (account.Equals(account1.Id))
                {
                    decimal limit = money;
                    foreach (KeyValuePair<decimal, decimal> pair in DictionaryDeposit)
                    {
                        if (pair.Key > money)
                        {
                            var depositScore = new DepositScore(money, pair.Value, limit, account1.Client, DateTime.Now);
                            account1.Scores.Add(depositScore);
                            return depositScore;
                        }
                    }
                }
            }

            throw new BanksException("failed to create deposit score");
        }

        public void ChargePercent(int days)
        {
            foreach (BankAccount score in Accounts.SelectMany(account => account.Scores))
            {
                score.ChargePercent(days);
            }
        }

        public void RaiseMoney(string id, decimal money)
        {
            foreach (BankAccount score in Accounts.Where(account => account.Id.Equals(id)).SelectMany(account => account.Scores))
            {
                score.RaiseMoney(money);
            }
        }

        public void PutMoney(string idAccount, decimal money, string idBankAccount)
        {
            Account account = Accounts.FirstOrDefault(account => account.Id.Equals(idAccount));
            BankAccount score = account.Scores.FirstOrDefault(score => score.NumberScore.Equals(idBankAccount));
            score.PutMoney(money);
        }

        public BankAccount SearchScore(string id)
        {
            foreach (Account account in Accounts)
            {
                foreach (var score in account.Scores)
                {
                    if (score.NumberScore.Equals(id))
                    {
                        return score;
                    }
                }
            }

            return null;
        }

        public Transactions Transaction(string idAccount1, string idAccount2, decimal sum)
        {
            BankAccount score = SearchScore(idAccount1);
            BankAccount score1 = SearchScore(idAccount2);
            score1.ScoreMoney += sum;
            score.ScoreMoney -= sum;
            var transaction = new Transactions(idAccount1, sum, idAccount2);
            TransactionsList.Add(transaction);
            return transaction;
        }

        public void CancelTransaction(string id)
        {
            foreach (Transactions transaction in TransactionsList)
            {
                if (transaction.Id.Equals(id))
                {
                    BankAccount score = SearchScore(transaction.NumberScoreSender);
                    BankAccount score1 = SearchScore(transaction.NumberScoreBeneficiary);
                    score.ScoreMoney += transaction.Sum;
                    score1.ScoreMoney -= transaction.Sum;
                }
            }
        }

        public void CancelNotify(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public DateTime AddDays(int days)
        {
            CurrentDate = CurrentDate.AddDays(days);
            return CurrentDate;
        }
    }
}