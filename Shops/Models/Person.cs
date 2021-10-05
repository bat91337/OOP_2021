namespace Shops.Models
{
    public class Person
    {
        public Person(string name, int money)
        {
            NamePerson = name;
            Wallet += money;
        }

        public decimal Wallet { get; set; }
        public string NamePerson { get; }
    }
}