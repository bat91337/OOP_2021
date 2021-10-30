using System.Linq;
using Shops.Models;
using Shops.Tools;
using NUnit.Framework;

namespace Shops.Tests
{
    public class ShopsTest
    {
        private ShopManager _shopManager;
        [SetUp]
        public void Setup()
        {
            
            _shopManager = new ShopManager();
        }
        [Test]
        public void AddProductToShop_ProductHasShopAndShopContainsProduct()
        {
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            Product product = _shopManager.AddProduct(123, 60, "apple", shop);
            Assert.Contains(product, shop.Products);
        }

        [Test]
        public void ChangePrice_PriceGanged()
        {
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            Product product = _shopManager.AddProduct(123, 60, "apple", shop);
            shop.ChangePrice(product, 134);
        }

        [Test]
        public void SearhProduct_ProductFound()
        {
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            Product product = _shopManager.AddProduct(123, 60, "apple", shop);
            Shop shop1 = _shopManager.AddShop("pyaterochka", "dawdaw");
            Product product1 = _shopManager.AddProduct(120, 60, "apple", shop1);
            _shopManager.ProductSearchMinimalPrice("apple", 50);
        }

        [Test]
        public void BuyProduct_ProductBought()
        {
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            Product product = _shopManager.AddProduct(1180, 60, "apple", shop);
             Shop shop1 = _shopManager.AddShop("magnit", "dawdawda");
             Product product1 = _shopManager.AddProduct(1240, 60, "apple", shop1);
            Shop shop2 = _shopManager.AddShop("perecrestok", "dawdawda");
             Product product2 = _shopManager.AddProduct(117, 60, "apple", shop2);
            var vasya = new Person("vasya", 120);
            _shopManager.Buy("apple", 1, vasya);
        }
    }
}