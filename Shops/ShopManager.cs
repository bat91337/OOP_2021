using System.Collections.Generic;
using System.Linq;
using Shops.Repositories;
using Shops.Tools;

namespace Shops.Models
{
    public class ShopManager
    {
        private readonly RepositoryListUniversumProduct _listproducts;
        private readonly RepositoryListShop _repositoryListShops;
        public ShopManager(RepositoryListUniversumProduct listproduct, RepositoryListShop repositoryListShops)
        {
            _listproducts = listproduct;
            _repositoryListShops = repositoryListShops;
        }

        public UniversumProducts AddInAllProduct(string name)
        {
            var product = new UniversumProducts(name);
            _listproducts.AddAllProduct(product);
            return product;
        }

        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address);
            _repositoryListShops.AddShops(shop);
            return shop;
        }

        public Product AddProductInShop(decimal price, int amount, UniversumProducts product1, Shop shop)
        {
            Shop shop1 = _repositoryListShops.GetListShop().First(shop1 => shop1.ShopId.Equals(shop.ShopId));
            {
                var product = new Product(product1, price, amount);
                shop1.Product.Add(product);
                return product;
            }
        }

        public Shop FindMinimalPrice(Product product, int amount)
        {
            decimal price1 = decimal.MaxValue;
            Shop shop1 = null;
            foreach (Shop shop in _repositoryListShops.GetListShop())
            {
                foreach (Product product1 in shop.Product.Where(product1 => product1.Products.ProductId.Equals(product.Products.ProductId)))
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

        public PersonProduct AddProductInBasket(UniversumProducts products, int amount, Person person)
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
            foreach (PersonProduct personProduct in basket)
            {
                foreach (Shop shop1 in _repositoryListShops.GetListShop())
                {
                    if (shop1.ShopId.Equals(shop.ShopId))
                    {
                        foreach (Product product1 in shop1.Product.Where(product1 => product1.Products.ProductId.Equals(personProduct.Products.ProductId)))
                        {
                            if (product1.ProductAmount >= personProduct.ProductAmount)
                            {
                                decimal price = product1.ProductPrice;
                                sum += price;
                            }
                            else
                            {
                                return -1;
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
            foreach (Shop shop in _repositoryListShops.GetListShop())
            {
                decimal sum = CountSumForProducts(shop, basket);
                if (sum.Equals(-1))
                {
                    continue;
                }

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
            Shop shop = FindMinimalPrices(basket);
            foreach (PersonProduct product in basket)
            {
                decimal sum = 0;
                Product product1 = shop.Product.FirstOrDefault(product1 => product.Products.ProductId.Equals(product1.Products.ProductId));
                decimal sumForOneProduct = product.ProductAmount * product1.ProductPrice;
                sum += sumForOneProduct;
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