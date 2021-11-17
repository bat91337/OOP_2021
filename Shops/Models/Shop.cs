using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class Shop
    {
        private static int _id;
        public Shop(string name, string shopAddress)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ShopsException("empty line");
            }

            NameShop = name;
            if (string.IsNullOrWhiteSpace(shopAddress))
            {
                throw new ShopsException("empty line");
            }

            ShopAddress = shopAddress;
            ShopId = ++_id;
        }

        public List<Product> Products { get; } = new List<Product>();
        public string NameShop { get; }
        public string ShopAddress { get; }

        public int ShopId { get; }
        public void ChangePrice(Product product, decimal newprice)
        {
            if (newprice == 0)
            {
                throw new ShopsException("the price cannot be zero");
            }

            if (newprice < 0)
            {
                throw new ShopsException("the price cannot be less than zero");
            }

            Product product1 = Products.First(product1 => product1.Productses.ProductId.Equals(product.Productses.ProductId));
            {
                product1.ProductPrice = newprice;
            }
        }

        public void Buy(Product product, int amount, Person person)
        {
            Product product1 = Products.First(product1 => product1.Productses.ProductId.Equals(product.Productses.ProductId));
            {
                    if (product1.ProductAmount >= amount)
                    {
                        decimal sum = amount * product1.ProductPrice;
                        if (sum <= person.PersonWallet)
                        {
                            person.BuyProduct(sum);
                            product1.ProductAmount -= amount;
                        }
                        else
                        {
                            throw new ShopsException("not enough money");
                        }
                    }
                    else
                    {
                        throw new ShopsException("not enough amount");
                    }
            }
        }
    }
}