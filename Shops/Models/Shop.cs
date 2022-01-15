using System.Collections.Generic;
using System.Linq;
using Shops.Repositories;
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

        public List<ShopProduct> Product { get; } = new List<ShopProduct>();
        public string NameShop { get; }
        public string ShopAddress { get; }

        public int ShopId { get; }
        public void ChangePrice(ShopProduct shopProduct, decimal newprice)
        {
            if (newprice == 0)
            {
                throw new ShopsException("the price cannot be zero");
            }

            if (newprice < 0)
            {
                throw new ShopsException("the price cannot be less than zero");
            }

            ShopProduct product1 = Product.FirstOrDefault(product1 => product1.Products.ProductId.Equals(shopProduct.Products.ProductId));
            product1.ProductPrice = newprice;
        }

        public void Buy(ShopProduct shopProduct, int amount, Person person)
        {
            ShopProduct product1 = Product.First(product1 => product1.Products.ProductId.Equals(shopProduct.Products.ProductId));
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