using System.Collections.Generic;
namespace Shops.Models
{
    public class Person
    {
        public Person(string personName, int money)
        {
            PersonName = personName;
            PersonWallet += money;
            PersonBasket = new Basket();
        }

        public decimal PersonWallet { get; set; }
        public string PersonName { get; }
        public Basket PersonBasket { get; }
    }
}