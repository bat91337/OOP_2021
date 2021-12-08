using System.Collections.Generic;

namespace Banks
{
    public class Client : IObserver
    {
        public Client(string firsNameClient, string lastNameClient, string passport, string address = "", string numberPhone = "")
        {
            FirstNameClient = firsNameClient;
            LastNameClient = lastNameClient;
            Address = address;
            Passport = passport;
            Message = new List<string>();
            NumberPhone = numberPhone;
        }

        public string LastNameClient { get; }
        public string Address { get; set; }
        public string Passport { get; }
        public string FirstNameClient { get; }
        public List<string> Message { get; }
        public string NumberPhone { get; set; }

        public void Update(string str)
        {
            Message.Add(str);
        }
    }
}