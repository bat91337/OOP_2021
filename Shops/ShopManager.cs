using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class ShopManager
    {
        private static int _shopId;
        private readonly List<Shop> _listShops = new List<Shop>();
        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address, ++_shopId);
            _listShops.Add(shop);
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

            foreach (Shop shop1 in _listShops)
            {
                if (shop1.NameShop.Equals(shop.NameShop))
                {
                    var product = new Product(name, price, amount, shop.NameShop);
                    shop1.Products.Add(product);
                    product.Amount += amount;
                    return product;
                }
            }

            return null;
        }

        public Shop ProductSearchMinimalPrice(string product, int amount)
        {
            decimal price1 = decimal.MaxValue;
            Shop shop1 = null;
            foreach (Shop shop in _listShops)
            {
                foreach (Product product1 in shop.Products.Where(product1 => product1.NameProduct.Equals(product)))
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

            return shop1;
        }

        public void Buy(string product, int amount, Person person)
        {
            Shop shop = ProductSearchMinimalPrice(product, amount);
            shop.Buy(product, amount, person);
        }
    }
}