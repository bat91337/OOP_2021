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

        public void ChangePercentCreditScore(CreditScore score)
        {
            string message = "your percent has changed to" + score.Percent;
            NotifyObservers(message);
        }

        public void ChangePercentDebitScore(DebitScore score)
        {
            string message = "your percent has changed to" + score.Percent;
            NotifyObservers(message);
        }

        public void ChangePercentDepositScore(DepositScore score)
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

        public Client CreateClient(Client client)
        {
            var clientBuilder = new ClientBuilder();
            Client client1 = clientBuilder.Build();
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
            foreach (Account account in Accounts.Where(account => id.Equals(account.Id)))
            {
                money += LimitCreditScore;
                var creditScore = new CreditScore(money, PercentCreditScore, LimitCreditScore, account.Client, DateTime.Now);
                account.Scores.Add(creditScore);
                return creditScore;
            }

            throw new BanksException("failed to create credit score");
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
                var transaction = new Transactions(id, money);
                TransactionsList.Add(transaction);
            }
        }

        public void PutMoney(string idAccount, decimal money, string idBankAccount)
        {
            Account account = Accounts.FirstOrDefault(account => account.Id.Equals(idAccount));
            BankAccount score = account.Scores.FirstOrDefault(score => score.NumberScore.Equals(idBankAccount));
            score.PutMoney(money);
            var transaction = new Transactions(idAccount, money);
            TransactionsList.Add(transaction);
        }

        public BankAccount SearchScore(string id)
        {
            foreach (Account account in Accounts)
            {
                BankAccount score = account.Scores.FirstOrDefault(bankAccount => bankAccount.NumberScore.Equals(id));
                return score;
            }

            return null;
        }

        public Transactions Transaction(string idBankAccount, string idAccount, decimal sum)
        {
            BankAccount score = SearchScore(idBankAccount);
            BankAccount score1 = SearchScore(idAccount);
            score1.ScoreMoney += sum;
            score.ScoreMoney -= sum;
            var transaction = new Transactions(idBankAccount, sum, idAccount);
            TransactionsList.Add(transaction);
            return transaction;
        }

        public void CancelTransaction(string id)
        {
            foreach (Transactions transaction in TransactionsList)
            {
                if (string.IsNullOrWhiteSpace(transaction.NumberScoreNew))
                {
                    throw new BanksException("money cannot be returned");
                }

                if (transaction.Id.Equals(id))
                {
                    BankAccount score = SearchScore(transaction.NumberScore);
                    BankAccount score1 = SearchScore(transaction.NumberScoreNew);
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