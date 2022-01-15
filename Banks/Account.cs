using System;
using System.Collections.Generic;

namespace Banks
{
    public class Account
    {
        public Account(Client client)
        {
            Scores = new List<BankAccount>();
            Client = client;
            Id = Guid.NewGuid().ToString();
        }

        public List<BankAccount> Scores { get; }
        public Client Client { get; set; }
        public string Id { get; set; }
    }
}