using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Banks.Scores;

namespace Banks
{
    public class Bank : IObservable, IObserver
    {
        public Bank(decimal percentDebitScore, decimal percentCreditScore, string name, decimal limit)
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
        }

        public List<Client> Client { get; }
        public List<IObserver> Observers { get; }
        public List<string> Message { get; }
        public decimal PercentDebitScore { get; }
        public decimal PercentCreditScore { get; }
        public decimal LimitCreditScore { get; }
        public string Name { get; }
        public string Id { get; }
        public List<Account> Accounts { get; }
        public DateTime CurrentDate { get; private set; }
        public Dictionary<decimal, decimal> DictionaryDeposit { get; set; }
        public List<Transactions> TransactionsList { get; }

        public void ChangePercentCreditScore(decimal percent, CreditScore score)
        {
            string message = "your percent has changed to";
            NotifyObservers(message);
        }

        public void ChangePercentDebitScore(decimal percent, DebitScore score)
        {
            string message = "your percent has changed to";
            NotifyObservers(message);
        }

        public void ChangePercentDepositScore(decimal percent, DepositScore score)
        {
            string message = "your percent has changed to" + score.Percent;
            NotifyObservers(message);
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

        public Client CreateClient(string firstName, string lastName, string passport, string address, string numberPhone)
        {
            var clientBuilder = new ClientBuilder();
            clientBuilder.SetFirstNAme(firstName);
            clientBuilder.SetName(lastName);
            clientBuilder.SetNumberPhone(passport);
            clientBuilder.SetAddress(address);
            clientBuilder.SetNumberPhone(numberPhone);
            Client client = clientBuilder.Build();
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
                    Console.WriteLine("incorrect data entered");
                }
                else
                {
                    client.Address = address;
                }
            }
        }

        public void AddNumberPhone(string passport, string numberPhone)
        {
            foreach (Client client in Client.Where(client => client.Passport.Equals(passport)))
            {
                if (string.IsNullOrWhiteSpace(numberPhone))
                {
                    Console.WriteLine("incorrect data entered");
                }
                else
                {
                    client.NumberPhone = numberPhone;
                }
            }
        }

        public CreditScore CreateCreditScore(string account, decimal money)
        {
            foreach (Account account1 in Accounts.Where(account1 => account.Equals(account1.Id)))
            {
                money += LimitCreditScore;
                var creditScore = new CreditScore(money, PercentCreditScore, LimitCreditScore, account1.Client, DateTime.Now);
                account1.Scores.Add(creditScore);
                return creditScore;
            }

            return null;
        }

        public DebitScore CreateDebitScore(string account, decimal money)
        {
            foreach (Account account1 in Accounts.Where(account1 => account.Equals(account1.Id)))
            {
                const decimal limit = 0;
                var debitScore = new DebitScore(money, PercentDebitScore, limit, account1.Client, DateTime.Now);
                account1.Scores.Add(debitScore);
                return debitScore;
            }

            return null;
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

            return null;
        }

        public void ChargePercent()
        {
            foreach (BankAccount score in Accounts.SelectMany(account => account.Scores))
            {
                score.ChargePercent(CurrentDate,  score.DateTime);
            }
        }

        public void RaiseMoney(string id, decimal money)
        {
            foreach (BankAccount score in Accounts.Where(account => account.Id.Equals(id)).SelectMany(account => account.Scores))
            {
                score.RaiseMoney(money);
                var transaction = new Transactions(id, money);
                TransactionsList.Add(transaction);
            }
        }

        public void PutMoney(string id, decimal money)
        {
            foreach (BankAccount score in Accounts.Where(account => account.Id.Equals(id)).SelectMany(account => account.Scores))
            {
                score.PutMoney(money);
                var transaction = new Transactions(id, money);
                TransactionsList.Add(transaction);
            }
        }

        public BankAccount SearchScore(string id1)
        {
            foreach (Account account in Accounts)
            {
                foreach (BankAccount score in account.Scores.Where(score => score.Id.Equals(id1)))
                {
                    return score;
                }
            }

            return null;
        }

        public Transactions Transaction(string account, string id, string id1, decimal sum)
        {
            foreach (Account accounts in Accounts)
            {
                if (accounts.Id.Equals(account))
                {
                    foreach (BankAccount score in accounts.Scores)
                    {
                        if (score.Id.Equals(id))
                        {
                            BankAccount score1 = SearchScore(id1);
                            score1.ScoreMoney += sum;
                            score.ScoreMoney -= sum;
                            var transaction = new Transactions(id, sum, id1);
                            TransactionsList.Add(transaction);
                            return transaction;
                        }
                    }
                }
            }

            return null;
        }

        public void CancelTransaction(string id)
        {
            foreach (Transactions transaction in TransactionsList)
            {
                if (transaction.Id.Equals(id))
                {
                    BankAccount score = SearchScore(transaction.NumberScore);
                    BankAccount score1 = SearchScore(transaction.NumberScoreNew);
                    score.ScoreMoney += transaction.Sum;
                    score1.ScoreMoney -= transaction.Sum;
                }
            }
        }

        public DateTime AddDays(int days)
        {
            CurrentDate = CurrentDate.AddDays(days);
            return CurrentDate;
        }
    }
}