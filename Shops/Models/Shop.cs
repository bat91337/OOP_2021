using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class Shop
    {
        public Shop()
        {
        }

        public Shop(string name, string shopAddress, int shopId)
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
            ShopId = shopId;
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

            foreach (Product product1 in Products.Where(product1 => product1.Products.ProductId.Equals(product.Products.ProductId)))
            {
                product1.ProductPrice = newprice;
            }
        }

        public void Buy(string product, int id, int amount, Person person)
        {
            foreach (Product product1 in Products)
            {
                if (product1.Products.ProductName.Equals(product))
                {
                    if (product1.Products.ProductId.Equals(id))
                    {
                        decimal sum = amount * product1.ProductPrice;
                        if (sum <= person.PersonWallet)
                        {
                            person.PersonWallet -= sum;
                            product1.ProductAmount -= amount;
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
}