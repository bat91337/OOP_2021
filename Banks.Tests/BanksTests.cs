using System;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTests
    {
        private CentralBank _centralBank;
        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
        }

        [Test]
        public void ChargePercentForCreditScore()
        {
            Bank bank = _centralBank.CreateBank("сбер", 1, 2, 7000, 50000, 3);
            bank.DictionaryDeposit.Add(10000, 4);
            bank.DictionaryDeposit.Add(10001, 5);
            Client client = bank.CreateClient("vasya", "ivanov", "123456789",  "pushkina 13", "123456789");
            Account account = bank.CreateAccount(client);
            BankAccount bankAccount = bank.CreateCreditScore(account.Id, 0);
            bank.RaiseMoney(account.Id, 100);
            _centralBank.AddDays(30);
            bank.AddDays(30);
            bank.ChargePercent();
            Assert.AreEqual(bankAccount.ScoreMoney, 900);
        }

        [Test]
        public void Translation()
        {
            Bank bank = _centralBank.CreateBank("сбер", 1, 2, 7000, 50000, 3);
            bank.DictionaryDeposit.Add(10000, 4);
            bank.DictionaryDeposit.Add(10001, 5);
            Client client = bank.CreateClient("vasya", "ivanov", "123456789",  "pushkina 13", "123456789");
            Account account = bank.CreateAccount(client);
            BankAccount bankAccount = bank.CreateDebitScore(account.Id, 1000);
            Client client1 = bank.CreateClient("vasya", "ivanov", "123456789",  "pushkina 13", "123456789");
            Account account1 = bank.CreateAccount(client);
            BankAccount bankAccount1 = bank.CreateDebitScore(account.Id, 0);
            bank.Transaction(account.Id, bankAccount.Id, bankAccount1.Id, 600 );
            Assert.AreEqual(bankAccount.ScoreMoney, 400);
        }

        [Test]
        public void CancelTranslation()
        {
            Bank bank = _centralBank.CreateBank("сбер", 1, 2, 7000, 50000, 3);
            bank.DictionaryDeposit.Add(10000, 4);
            bank.DictionaryDeposit.Add(10001, 5);
            Client client = bank.CreateClient("vasya", "ivanov", "123456789",  "pushkina 13", "123456789");
            Account account = bank.CreateAccount(client);
            BankAccount bankAccount = bank.CreateDebitScore(account.Id, 1000);
            Client client1 = bank.CreateClient("vasya", "ivanov", "123456789",  "pushkina 13", "123456789");
            Account account1 = bank.CreateAccount(client);
            BankAccount bankAccount1 = bank.CreateDebitScore(account.Id, 0);
            Transactions translation = bank.Transaction(account.Id, bankAccount.Id, bankAccount1.Id, 600 );
            bank.CancelTransaction(translation.Id);
            Assert.AreEqual(bankAccount.ScoreMoney, 1000);
        }
    }
}