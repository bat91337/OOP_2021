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
            Console.WriteLine("enter customer name");
            string firstName = Console.ReadLine();
            Console.WriteLine("enter the client last name");
            string lastName = Console.ReadLine();
            Console.WriteLine("enter your passport number");
            string passport = Console.ReadLine();
            Console.WriteLine("enter address");
            string address = Console.ReadLine();
            Console.WriteLine("enter phone number");
            string numberPhone = Console.ReadLine();
            _bank.CreateClient();
        }

        public void CreateCreditScoreConsole()
        {
            Console.WriteLine("enter your account id");
            string account = Console.ReadLine();
            Console.WriteLine("how much money do you want to throw in?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.CreateCreditScore(account, money);
        }

        public void PutMoneyConsole()
        {
            Console.WriteLine("enter account number");
            string id = Console.ReadLine();
            Console.WriteLine("how much money do you want to put in?");
            decimal money = decimal.Parse(Console.ReadLine());
            Console.WriteLine("how much money do you want to withdraw?");
            string idBankAccount = Console.ReadLine();
            _bank.PutMoney(id, money, idBankAccount);
        }

        public void RaiseMoneyConsole()
        {
            Console.WriteLine("enter account number");
            string id = Console.ReadLine();
            Console.WriteLine("how much money do you want to withdraw?");
            decimal money = decimal.Parse(Console.ReadLine());
            Console.WriteLine("how much money do you want to withdraw?");
            string idBankAccount = Console.ReadLine();
            _bank.RaiseMoney(id, money);
        }

        public void TransactionConsole()
        {
            Console.WriteLine("enter account number");
            string account = Console.ReadLine();
            Console.WriteLine("Enter your account number");
            string id = Console.ReadLine();
            Console.WriteLine("enter the account number to which you want to transfer money");
            string id1 = Console.ReadLine();
            Console.WriteLine("enter the amount");
            decimal sum = decimal.Parse(Console.ReadLine());
            _bank.Transaction(account, id, sum);
        }

        public void CreateDebitScoreConsole()
        {
            Console.WriteLine("enter your account id");
            string account = Console.ReadLine();
            Console.WriteLine("how much money do you want to throw in?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.CreateDebitScore(account, money);
        }

        public void CancelTransactionConsole()
        {
            Console.WriteLine("enter the id of the transaction");
            string id = Console.ReadLine();
            _bank.CancelTransaction(id);
        }

        public void CreateDepositScoreConsole()
        {
            Console.WriteLine("enter your account id");
            string account = Console.ReadLine();
            Console.WriteLine("how much money do you want to throw in?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.CreateDepositScore(account, money);
        }

        public void AddNumberPhoneConsole()
        {
            Console.WriteLine("enter passport details");
            string passport = Console.ReadLine();
            Console.WriteLine("enter phone number");
            string numberPhone = Console.ReadLine();
            _bank.AddNumberPhone(passport, numberPhone);
        }

        public void AddAddressConsole()
        {
            Console.WriteLine("enter passport details");
            string passport = Console.ReadLine();
            Console.WriteLine("enter address");
            string address = Console.ReadLine();
            _bank.AddAddress(passport, address);
        }
    }
}