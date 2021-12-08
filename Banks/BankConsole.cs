using System;

namespace Banks
{
    public class BankConsole
    {
        private CentralBank _centralBank;
        private Bank _bank;
        public BankConsole(CentralBank centralBank, Bank bank)
        {
            _centralBank = centralBank;
            _bank = bank;
        }

        public void CreateClientConsole()
        {
            Console.WriteLine("введите имя клиента");
            string firstName = Console.ReadLine();
            Console.WriteLine("введите фамилию клиента");
            string lastName = Console.ReadLine();
            Console.WriteLine("введите номер паспорта");
            string passport = Console.ReadLine();
            Console.WriteLine("введите адрес");
            string address = Console.ReadLine();
            Console.WriteLine("введите номер телефона");
            string numberPhone = Console.ReadLine();
            _bank.CreateClient(firstName, lastName, passport, address, numberPhone);
        }

        public void CreateCreditScoreConsole()
        {
            Console.WriteLine("введите id аккаунта");
            string account = Console.ReadLine();
            Console.WriteLine("сколько денег хотите закинуть?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.CreateCreditScore(account, money);
        }

        public void PutMoneyConsole()
        {
            Console.WriteLine("введите номер счета");
            string id = Console.ReadLine();
            Console.WriteLine("сколько денег вы хотите положить?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.PutMoney(id, money);
        }

        public void RaiseMoneyConsole()
        {
            Console.WriteLine("введите номер счета");
            string id = Console.ReadLine();
            Console.WriteLine("сколько денег вы хотите снять?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.RaiseMoney(id, money);
        }

        public void TransactionConsole()
        {
            Console.WriteLine("введите номер аккаунта");
            string account = Console.ReadLine();
            Console.WriteLine("Введите ваш номер счета");
            string id = Console.ReadLine();
            Console.WriteLine("введите номер счета на который хотите перевести деньги");
            string id1 = Console.ReadLine();
            Console.WriteLine("введите сумму");
            decimal sum = decimal.Parse(Console.ReadLine());
            _bank.Transaction(account, id, id1, sum);
        }

        public void CreateDebitScoreConsole()
        {
            Console.WriteLine("введите id аккаунта");
            string account = Console.ReadLine();
            Console.WriteLine("сколько денег хотите закинуть?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.CreateDebitScore(account, money);
        }

        public void CancelTransactionConsole()
        {
            Console.WriteLine("введите id транзакции");
            string id = Console.ReadLine();
            _bank.CancelTransaction(id);
        }

        public void CreateDepositScoreConsole()
        {
            Console.WriteLine("введите id аккаунта");
            string account = Console.ReadLine();
            Console.WriteLine("сколько денег хотите закинуть?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.CreateDepositScore(account, money);
        }

        public void AddNumberPhoneConsole()
        {
            Console.WriteLine("введите данные паспорта");
            string passport = Console.ReadLine();
            Console.WriteLine("введите номер телефона");
            string numberPhone = Console.ReadLine();
            _bank.AddNumberPhone(passport, numberPhone);
        }

        public void AddAddressConsole()
        {
            Console.WriteLine("введите данные паспорта");
            string passport = Console.ReadLine();
            Console.WriteLine("введите адрес");
            string address = Console.ReadLine();
            _bank.AddAddress(passport, address);
        }
    }
}