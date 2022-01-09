using System;

namespace Banks
{
    internal class Program
    {
        private static CentralBank _centralBank;
        private static Bank _bank;
        public Program(CentralBank centralBank, Bank bank)
        {
            _centralBank = centralBank;
            _bank = bank;
        }

        public static void CreateClientConsole()
        {
            var clientBuilder = new ClientBuilder();
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
            clientBuilder
                .SetName(lastName)
                .SetFirstNAme(firstName)
                .SetPassport(passport)
                .SetAddress(address)
                .SetNumberPhone(numberPhone);
            _bank.CreateClient(clientBuilder.Build());
        }

        public static void CreateCreditScoreConsole()
        {
            Console.WriteLine("enter your account id");
            string account = Console.ReadLine();
            Console.WriteLine("how much money do you want to throw in?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.CreateCreditScore(account, money);
        }

        public static void PutMoneyConsole()
        {
            Console.WriteLine("enter account number");
            string id = Console.ReadLine();
            Console.WriteLine("how much money do you want to put in?");
            decimal money = decimal.Parse(Console.ReadLine());
            Console.WriteLine("how much money do you want to withdraw?");
            string idBankAccount = Console.ReadLine();
            _bank.PutMoney(id, money, idBankAccount);
        }

        public static void RaiseMoneyConsole()
        {
            Console.WriteLine("enter account number");
            string id = Console.ReadLine();
            Console.WriteLine("how much money do you want to withdraw?");
            decimal money = decimal.Parse(Console.ReadLine());
            Console.WriteLine("how much money do you want to withdraw?");
            string idBankAccount = Console.ReadLine();
            _bank.RaiseMoney(id, money);
        }

        public static void TransactionConsole()
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

        public static void CreateDebitScoreConsole()
        {
            Console.WriteLine("enter your account id");
            string account = Console.ReadLine();
            Console.WriteLine("how much money do you want to throw in?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.CreateDebitScore(account, money);
        }

        public static void CancelTransactionConsole()
        {
            Console.WriteLine("enter the id of the transaction");
            string id = Console.ReadLine();
            _bank.CancelTransaction(id);
        }

        public static void CreateDepositScoreConsole()
        {
            Console.WriteLine("enter your account id");
            string account = Console.ReadLine();
            Console.WriteLine("how much money do you want to throw in?");
            decimal money = decimal.Parse(Console.ReadLine());
            _bank.CreateDepositScore(account, money);
        }

        public static void AddNumberPhoneConsole()
        {
            Console.WriteLine("enter passport details");
            string passport = Console.ReadLine();
            Console.WriteLine("enter phone number");
            string numberPhone = Console.ReadLine();
            _bank.AddNumberPhone(passport, numberPhone);
        }

        public static void AddAddressConsole()
        {
            Console.WriteLine("enter passport details");
            string passport = Console.ReadLine();
            Console.WriteLine("enter address");
            string address = Console.ReadLine();
            _bank.AddAddress(passport, address);
        }

        public void CreateCentralBank(string bankName, decimal percentDebitScore, decimal percentCreditScore, decimal percentDepositScore)
        {
            _centralBank = CentralBank.GetInstance(bankName, percentDebitScore, percentCreditScore, percentDepositScore);
        }

        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("1 - register client");
                Console.WriteLine("2 - create a credit account");
                Console.WriteLine("3 - create a debit account");
                Console.WriteLine("4 - create a deposit account");
                Console.WriteLine("5 - withdraw money from the account");
                Console.WriteLine("6 - put money into the account");
                Console.WriteLine("7 - Transaction money to another account");
                Console.WriteLine("8 - cancel Transaction");
                Console.WriteLine("9 - add address");
                Console.WriteLine("10 - Add number phone");
                Console.WriteLine("11 - exit");
                string str = Console.ReadLine();
                int number = Convert.ToInt32(str);
                switch (number)
                {
                    case 1:
                        CreateClientConsole();
                        continue;
                    case 2:
                        CreateCreditScoreConsole();
                        continue;
                    case 3:
                        CreateDebitScoreConsole();
                        continue;
                    case 4:
                        CreateDepositScoreConsole();
                        continue;
                    case 5:
                        RaiseMoneyConsole();
                        continue;
                    case 6:
                        PutMoneyConsole();
                        continue;
                    case 7:
                        TransactionConsole();
                        continue;
                    case 8:
                        CancelTransactionConsole();
                        continue;
                    case 9:
                        AddAddressConsole();
                        continue;
                    case 10:
                        AddNumberPhoneConsole();
                        continue;
                    case 11:
                        return;
                    default:
                        Console.WriteLine("wrong number entered");
                        continue;
                }
            }
        }
    }
}
