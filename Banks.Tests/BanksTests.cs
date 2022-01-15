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
            Bank bank = _centralBank.CreateBank("сбер", 1, 2, 7000, 50000, 3, 1);
            bank.DictionaryDeposit.Add(10000, 4);
            bank.DictionaryDeposit.Add(10001, 5);
            var clientBuilder = new ClientBuilder();
            clientBuilder
                .SetAddress("dasd")
                .SetName("dasdaw")
                .SetPassport("dasda")
                .SetNumberPhone("dawdawd")
                .SetFirstNAme("dwdsdsffd");
            Client client =clientBuilder.Build();
            bank.CreateClient(client);
            Account account = bank.CreateAccount(client);
            BankAccount bankAccount = bank.CreateCreditScore(account.Id, 0);
            bank.RaiseMoney(account.Id, 100);
            _centralBank.AddDays(30);
            bank.AddDays(30);
            bank.ChargePercent(30);
            Assert.AreEqual(bankAccount.ScoreMoney, 900);
        }

        [Test]
        public void Transaction()
        {
            Bank bank = _centralBank.CreateBank("сбер", 1, 2, 7000, 50000, 3, 1);
            bank.DictionaryDeposit.Add(10000, 4);
            bank.DictionaryDeposit.Add(10001, 5);
            var clientBuilder = new ClientBuilder();
            clientBuilder
                .SetAddress("dasd")
                .SetName("dasdaw")
                .SetPassport("dasda")
                .SetNumberPhone("dawdawd")
                .SetFirstNAme("dwdsdsffd");
            Client client =clientBuilder.Build();
            bank.CreateClient(client);
            Account account = bank.CreateAccount(client);
            BankAccount bankAccount = bank.CreateDebitScore(account.Id, 1000);
            var clientBuilder1 = new ClientBuilder();
            clientBuilder1
                .SetAddress("dasd")
                .SetName("dasdaw")
                .SetPassport("dasda")
                .SetNumberPhone("dawdawd")
                .SetFirstNAme("dwdsdsffd");
            Client client1 =clientBuilder1.Build();
            bank.CreateClient(client1);
            Account account1 = bank.CreateAccount(client);
            BankAccount bankAccount1 = bank.CreateDebitScore(account.Id, 0);
            bank.Transaction(bankAccount.NumberScore, bankAccount1.NumberScore, 600 );
            Assert.AreEqual(bankAccount.ScoreMoney, 400);
        }

        [Test]
        public void CancelTransaction()
        {
            Bank bank = _centralBank.CreateBank("сбер", 1, 2, 7000, 50000, 3, 1);
            bank.DictionaryDeposit.Add(10000, 4);
            bank.DictionaryDeposit.Add(10001, 5);
            var clientBuilder = new ClientBuilder();
            clientBuilder
                .SetAddress("dasd")
                .SetName("dasdaw")
                .SetPassport("dasda")
                .SetNumberPhone("dawdawd")
                .SetFirstNAme("dwdsdsffd");
            Client client =clientBuilder.Build();
            bank.CreateClient(client);
            Account account = bank.CreateAccount(client);
            BankAccount bankAccount = bank.CreateDebitScore(account.Id, 1000);
            var clientBuilder1 = new ClientBuilder();
            clientBuilder1
                .SetAddress("dasd")
                .SetName("dasdaw")
                .SetPassport("dasda")
                .SetNumberPhone("dawdawd")
                .SetFirstNAme("dwdsdsffd");
            Client client1 =clientBuilder1.Build();
            bank.CreateClient(client1);
            Account account1 = bank.CreateAccount(client1);
            BankAccount bankAccount1 = bank.CreateDebitScore(account.Id, 0);
            Transactions translation = bank.Transaction(bankAccount.NumberScore, bankAccount1.NumberScore, 600 );
            bank.CancelTransaction(translation.Id);
            Assert.AreEqual(bankAccount.ScoreMoney, 1000);
        }

        [Test]
        public void ChargePercentForDepositScore()
        {
            Bank bank = _centralBank.CreateBank("сбер", 1, 2, 7000, 5000, 3, 1);
            bank.DictionaryDeposit.Add(10000, 4);
            bank.DictionaryDeposit.Add(decimal.MaxValue, 5);
            var clientBuilder1 = new ClientBuilder();
            clientBuilder1
                .SetAddress("dasd")
                .SetName("dasdaw")
                .SetPassport("dasda")
                .SetNumberPhone("dawdawd")
                .SetFirstNAme("dwdsdsffd");
            Client client1 =clientBuilder1.Build();
            bank.CreateClient(client1);
            Account account = bank.CreateAccount(client1);
            BankAccount bankAccount = bank.CreateDepositScore(account.Id, 11000);
            _centralBank.AddDays(30);
            bank.AddDays(30);
            bank.ChargePercent(30);
            Assert.AreEqual(bankAccount.ScoreMoney, 1661000);
            
        }

        [Test]
        public void CreateCreditScoreTest()
        {
            Bank bank = _centralBank.CreateBank("сбер", 1, 2, 7000, 5000, 3, 1);
            bank.DictionaryDeposit.Add(10000, 4);
            bank.DictionaryDeposit.Add(decimal.MaxValue, 5);
            var clientBuilder1 = new ClientBuilder();
            clientBuilder1
                .SetAddress("dasd")
                .SetName("dasdaw")
                .SetPassport("dasda")
                .SetNumberPhone("dawdawd")
                .SetFirstNAme("dwdsdsffd");
            Client client1 =clientBuilder1.Build();
            bank.CreateClient(client1);
            Account account = bank.CreateAccount(client1);
            BankAccount bankAccount = bank.CreateCreditScore(account.Id, 11000);
            bank.RaiseMoney(account.Id, 11500);
            _centralBank.AddDays(30);
            bank.AddDays(30);
            bank.ChargePercent(30);
            Assert.AreEqual(bankAccount.ScoreMoney, -23500);
        }
    }
}