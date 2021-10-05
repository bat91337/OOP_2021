using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Models
{
    public class ShopManager
    {
        private static int _shopId = 0;
        private List<Shop> Listshops { get; set; } = new List<Shop>();
        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address, ++_shopId);
            Listshops.Add(shop);
            return shop;
        }

        public Product AddProduct(decimal price, int amount, string name, Shop shop)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ShopsException("error");
            }

            if (price == 0)
            {
                throw new ShopsException("error");
            }

            if (amount == 0)
            {
                throw new ShopsException("error");
            }

            foreach (var shop1 in Listshops)
            {
                if (shop1.NameShop.Equals(shop.NameShop))
                {
                    var product = new Product(name, price, amount, shop.NameShop);
                    shop1.Products.Add(product);
                    amount += amount;
                    return product;
                }
            }

            return null;
        }

        public void ChangePrice(Shop name, Product product, decimal newprice)
        {
            if (newprice == 0)
            {
                throw new ShopsException("error");
            }

            foreach (var shop in Listshops)
            {
                if (shop.NameShop.Equals(name.NameShop))
                {
                    foreach (var product1 in shop.Products)
                    {
                        if (product1.NameProduct.Equals(product.NameProduct))
                        {
                            product1.Price = newprice;
                        }
                    }
                }
            }
        }

        public Shop ProductSearchMinimalPrice(string product, int amount)
        {
            decimal price1 = decimal.MaxValue;
            Shop shop1 = null;
            foreach (var shop in Listshops)
            {
                foreach (var product1 in shop.Products)
                {
                    if (product1.NameProduct.Equals(product))
                    {
                        if (product1.Amount >= amount)
                        {
                            if (product1.Price < price1)
                            {
                                 price1 = product1.Price;
                                 shop1 = shop;
                            }
                        }
                        else
                        {
                            throw new ShopsException("not enough ");
                        }
                    }
                }
            }

            return shop1;
        }

        public void Buy(string product, int amount, Person person)
        {
            decimal sum;
            Shop shop = ProductSearchMinimalPrice(product, amount);
            foreach (var product1 in shop.Products)
            {
                sum = amount * product1.Price;
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