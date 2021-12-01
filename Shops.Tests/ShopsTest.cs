using System.Collections.Generic;
using Shops.Models;
using NUnit.Framework;
using Shops.Repositories;

namespace Shops.Tests
{
    public class ShopsTest
    {
        private ShopManager _shopManager;
        private ProductRepository _productRepository;
        private ShopRepository _shopRepository;
        [SetUp]
        public void Setup()
        {
            _productRepository = new ProductRepository();
            _shopRepository = new ShopRepository();
            _shopManager = new ShopManager(_productRepository, _shopRepository);
        }
        [Test]
        public void AddProductToShop_ProductHasShopAndShopContainsProduct()
        {
             Product product = _shopManager.AddInAllProduct("apple");
             Product universumProduct1 = _shopManager.AddInAllProduct("rice");
             Shop shop = _shopManager.AddShop("magnit", "dawdawda");
             Shop shop1 = _shopManager.AddShop("peterochka", "dawdawda");
            ShopProduct shopProduct = _shopManager.AddProductInShop(123, 60, product, shop);
            Assert.Contains(shopProduct, shop.Product);
        }

        [Test]
        public void ChangePrice_PriceGanged()
        {
            Product product = _shopManager.AddInAllProduct("apple");
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            ShopProduct shopProduct = _shopManager.AddProductInShop(123, 60, product, shop);
            shop.ChangePrice(shopProduct, 134);
            Assert.AreEqual(shop.Product[0].ProductPrice, 134);
        }
        [Test]
        public void SearhProduct_ProductFound()
        {
            Product product = _shopManager.AddInAllProduct("apple");
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            ShopProduct shopProduct = _shopManager.AddProductInShop(121, 60, product, shop);
            Shop shop1 = _shopManager.AddShop("magnit", "dawdawda");
            ShopProduct product1 = _shopManager.AddProductInShop(123, 60, product, shop1);
            Shop productFound =  _shopManager.FindMinimalPrice(shopProduct, 50);
            Assert.AreEqual(productFound.ShopId, shop.ShopId);
        }
        [Test]
        public void BuyProduct_ProductBought()
        {
            Product product = _shopManager.AddInAllProduct("apple");
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            ShopProduct shopProduct = _shopManager.AddProductInShop(123, 60, product, shop);
            Shop shop1 = _shopManager.AddShop("magnit", "dawdawda");
            ShopProduct product1 = _shopManager.AddProductInShop(123, 60, product, shop1);
            Shop shop2 = _shopManager.AddShop("magnit", "dawdawda");
            ShopProduct product2 = _shopManager.AddProductInShop(123, 60, product, shop1);
            var vasya = new Person("vasya", 12000);
            _shopManager.BuyOneProduct(shopProduct, 1, vasya);
            Assert.AreEqual(vasya.PersonWallet, 11877);
        }
        [Test]
        public void FindProducts_ProductFound()
        {
            var vasya = new Person("vasya", 12000);
            Product product = _shopManager.AddInAllProduct("apple");
            Product universumProduct1 = _shopManager.AddInAllProduct("pear");
            PersonProduct personProduct = _shopManager.AddProductInBasket(product, 5, vasya);
            PersonProduct personProduct1 = _shopManager.AddProductInBasket(universumProduct1, 6, vasya);
            List<PersonProduct> listPersonProduct = _shopManager.GiveListBasket(vasya);
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            ShopProduct shopProduct = _shopManager.AddProductInShop(113, 60, product, shop);
            ShopProduct product1 = _shopManager.AddProductInShop(134, 60, universumProduct1, shop);
            Shop shop1 = _shopManager.AddShop("pyaterochka", "dawdawda");
            ShopProduct product11 = _shopManager.AddProductInShop(12, 1, product, shop1);
            ShopProduct product12 = _shopManager.AddProductInShop(11, 1, universumProduct1, shop1);
            Shop shop2 = _shopManager.AddShop("perekrestok", "dawdawda");
            ShopProduct product2 = _shopManager.AddProductInShop(123, 60, product, shop2);
            ShopProduct product21 = _shopManager.AddProductInShop(150, 60, universumProduct1, shop2);
            Shop shopWithMinimalPrice = _shopManager.FindMinimalPrices(listPersonProduct);
            Assert.AreEqual(shopWithMinimalPrice.ShopId, shop.ShopId);
        }
   
        [Test]
        public void BuyProducts_ProductsBought()
        {
            var vasya = new Person("vasya", 12);
            Product product = _shopManager.AddInAllProduct("apple");
            Product universumProduct1 = _shopManager.AddInAllProduct("pear");
            PersonProduct personProduct = _shopManager.AddProductInBasket(product, 4, vasya);
            PersonProduct personProduct1 = _shopManager.AddProductInBasket(universumProduct1, 2, vasya);
            List<PersonProduct> listPersonProduct = _shopManager.GiveListBasket(vasya);
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            ShopProduct shopProduct = _shopManager.AddProductInShop(1, 2, product, shop);
            ShopProduct product1 = _shopManager.AddProductInShop(1, 2, universumProduct1, shop);
            Shop shop1 = _shopManager.AddShop("pyaterochka", "dawdawda");
            ShopProduct product11 = _shopManager.AddProductInShop(2, 5, product, shop1);
            ShopProduct product12 = _shopManager.AddProductInShop(2, 5, universumProduct1, shop1);
            Shop shop2 = _shopManager.AddShop("perekrestok", "dawdawda");
            ShopProduct product2 = _shopManager.AddProductInShop(3, 60, product, shop2);
            ShopProduct product21 = _shopManager.AddProductInShop(3, 60, universumProduct1, shop2);
            _shopManager.BuyProducts(listPersonProduct, vasya);
            Assert.AreEqual(vasya.PersonWallet, 0);
        }
    }
}