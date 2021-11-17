using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class ShopManager
    {
        private readonly List<Shop> _listShops;
        private List<AllProducts> _listproduct;

        public ShopManager()
        {
            _listShops = new List<Shop>();
            _listproduct = new List<AllProducts>();
        }

        public AllProducts AddInAllProduct(string name)
        {
            var product = new AllProducts(name);
            _listproduct.Add(product);
            return product;
        }

        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address);
            _listShops.Add(shop);
            return shop;
        }

        public Product AddProductInShop(decimal price, int amount, AllProducts product1, Shop shop)
        {
            Shop shop1 = _listShops.First(shop1 => shop1.ShopId.Equals(shop.ShopId));
            {
                var product = new Product(product1, price, amount);
                shop1.Products.Add(product);
                product.ProductAmount += amount;
                return product;
            }
        }

        public Shop FindMinimalPrice(Product product, int amount)
        {
            decimal price1 = decimal.MaxValue;
            Shop shop1 = null;
            foreach (Shop shop in _listShops)
            {
                foreach (Product product1 in shop.Products.Where(product1 => product1.Productses.ProductId.Equals(product.Productses.ProductId)))
                {
                    if (product1.ProductAmount >= amount)
                    {
                        if (product1.ProductPrice <= price1)
                        {
                            price1 = product1.ProductPrice;
                            shop1 = shop;
                        }
                    }
                    else
                    {
                        throw new ShopsException("not enough");
                    }
                }
            }

            return shop1;
        }

        public void BuyOneProduct(Product product, int amount, Person person)
        {
            Shop shop = FindMinimalPrice(product, amount);
            shop.Buy(product, amount, person);
        }

        public PersonProduct AddProductInBasket(AllProducts products, int amount, Person person)
        {
            var products2 = new PersonProduct(products, amount);
            person.PersonBasket.ProductsInBasket.Add(products2);
            return products2;
        }

        public List<PersonProduct> GiveListBasket(Person person)
        {
            return person.PersonBasket.ProductsInBasket;
        }

        public decimal CountSumForProducts(Shop shop, List<PersonProduct> basket)
        {
            decimal sum = 0;
            foreach (PersonProduct product in basket)
            {
                foreach (Shop shop1 in _listShops)
                {
                    if (shop1.ShopId.Equals(shop.ShopId))
                    {
                        foreach (Product product1 in shop1.Products.Where(product1 => product1.Productses.ProductId.Equals(product.Products.ProductId)))
                        {
                            if (product1.ProductAmount > product.ProductAmount)
                            {
                                decimal price = product1.ProductPrice;
                                sum += price;
                            }
                        }
                    }
                }
            }

            return sum;
        }

        public Shop FindMinimalPrices(List<PersonProduct> basket)
        {
            var dictionaryMinimalPrice = new Dictionary<Shop, decimal>();
            decimal minPrice = 0;
            foreach (Shop shop in _listShops)
            {
                decimal sum = CountSumForProducts(shop, basket);
                dictionaryMinimalPrice.Add(shop, sum);
                minPrice = sum;
            }

            Shop resultShop = null;
            foreach (KeyValuePair<Shop, decimal> pair in dictionaryMinimalPrice.Where(pair => pair.Value < minPrice))
            {
                minPrice = pair.Value;
                resultShop = pair.Key;
            }

            return resultShop;
        }

        public void BuyProducts(List<PersonProduct> basket, Person person)
        {
            decimal sum = 0;
            Shop shop = FindMinimalPrices(basket);
            foreach (PersonProduct product in basket)
            {
                foreach (Product product1 in _listShops.Where(shop1 => shop.ShopId.Equals(shop1.ShopId)).SelectMany(shop1 => shop1.Products))
                {
                    if (product.Products.ProductId.Equals(product1.Productses.ProductId))
                    {
                        decimal sumForOneProduct = product.ProductAmount * product1.ProductPrice;
                        sum += sumForOneProduct;
                    }

                    if (sum <= person.PersonWallet)
                    {
                        person.BuyProduct(sum);
                        product1.ProductAmount -= product.ProductAmount;
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