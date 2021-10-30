using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class Shop
    {
        public Shop(string name, string address, int id)
        {
            NameShop = name;
            Address = address;
            Id = id;
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public string NameShop { get; }
        public string Address { get; }

        public int Id { get; }
        public void ChangePrice(Product product, decimal newprice)
        {
            if (newprice == 0)
            {
                throw new ShopsException("error");
            }

            foreach (Product product1 in Products.Where(product1 => product1.NameProduct.Equals(product.NameProduct)))
            {
                product1.Price = newprice;
            }
        }

        public void Buy(string product, int amount, Person person)
        {
            foreach (Product product1 in Products)
            {
                if (product1.NameProduct.Equals(product))
                {
                    decimal sum = amount * product1.Price;
                    if (sum <= person.Wallet)
                    {
                        person.Wallet -= sum;
                        product1.Amount -= amount;
                    }
                    else
                    {
                        throw new ShopsException("not enough money");
                    }
                }
            }
        }
    }
}