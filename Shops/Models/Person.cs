using Shops.Tools;
namespace Shops.Models
{
    public class Person
    {
        public Person(string personName, int money)
        {
            if (string.IsNullOrWhiteSpace(personName))
            {
                throw new ShopsException("empty line personName");
            }

            PersonName = personName;
            if (money <= 0)
            {
                throw new ShopsException("empty line personName");
            }

            if (money < PersonWallet)
            {
                throw new ShopsException("no empty money");
            }

            PersonWallet += money;
            PersonBasket = new Basket();
        }

        public decimal PersonWallet { get; private set; }
        public string PersonName { get; }
        public Basket PersonBasket { get; }
        public void BuyProduct(decimal sum)
        {
            PersonWallet -= sum;
        }
    }
}