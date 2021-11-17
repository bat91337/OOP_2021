using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
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
             AllProducts allProducts = _shopManager.AddInAllProduct("apple");
             AllProducts allProduct1 = _shopManager.AddInAllProduct("rice");
             Shop shop = _shopManager.AddShop("magnit", "dawdawda");
             Shop shop1 = _shopManager.AddShop("peterochka", "dawdawda");
            Product product = _shopManager.AddProductInShop(123, 60, allProducts, shop);
            Assert.Contains(product, shop.Products);
        }

        [Test]
        public void ChangePrice_PriceGanged()
        {
            AllProducts allProducts = _shopManager.AddInAllProduct("apple");
            Shop shop = _shopManager.AddShop("magnit", "dawdawda");
            Product product = _shopManager.AddProductInShop(123, 60, allProducts, shop);
            shop.ChangePrice(product, 134);
            Assert.AreEqual(shop.Products[0].ProductPrice, 134);
        }
    [Test]
    public void SearhProduct_ProductFound()
    {
        AllProducts allProduct = _shopManager.AddInAllProduct("apple");
        Shop shop = _shopManager.AddShop("magnit", "dawdawda");
        Product product = _shopManager.AddProductInShop(121, 60, allProduct, shop);
        Shop shop1 = _shopManager.AddShop("magnit", "dawdawda");
        Product product1 = _shopManager.AddProductInShop(123, 60, allProduct, shop1);
        Shop productFound =  _shopManager.FindMinimalPrice(product, 50);
        Assert.AreEqual(productFound.ShopId, shop.ShopId);
    }
    
    [Test]
    public void BuyProduct_ProductBought()
    {
        AllProducts allProducts = _shopManager.AddInAllProduct("apple");
        Shop shop = _shopManager.AddShop("magnit", "dawdawda");
        Product product = _shopManager.AddProductInShop(123, 60, allProducts, shop);
        Shop shop1 = _shopManager.AddShop("magnit", "dawdawda");
        Product product1 = _shopManager.AddProductInShop(123, 60, allProducts, shop1);
        Shop shop2 = _shopManager.AddShop("magnit", "dawdawda");
        Product product2 = _shopManager.AddProductInShop(123, 60, allProducts, shop1);
        var vasya = new Person("vasya", 12000);
        _shopManager.BuyOneProduct(product, 1, vasya);
        Assert.AreEqual(vasya.PersonWallet, 11877);
    }
    
    [Test]
    public void FindProducts_ProductFound()
    {
        var vasya = new Person("vasya", 12000);
        AllProducts allProduct = _shopManager.AddInAllProduct("apple");
        AllProducts allProduct1 = _shopManager.AddInAllProduct("pear");
        PersonProduct personProduct = _shopManager.AddProductInBasket(allProduct, 5, vasya);
        PersonProduct personProduct1 = _shopManager.AddProductInBasket(allProduct1, 6, vasya);
        List<PersonProduct> listPersonProduct = _shopManager.GiveListBasket(vasya);
        Shop shop = _shopManager.AddShop("magnit", "dawdawda");
        Product product = _shopManager.AddProductInShop(113, 60, allProduct, shop);
        Product product1 = _shopManager.AddProductInShop(134, 60, allProduct1, shop);
        Shop shop1 = _shopManager.AddShop("pyaterochka", "dawdawda");
        Product product11 = _shopManager.AddProductInShop(123, 60, allProduct, shop1);
        Product product12 = _shopManager.AddProductInShop(115, 60, allProduct1, shop1);
        Shop shop2 = _shopManager.AddShop("perekrestok", "dawdawda");
        Product product2 = _shopManager.AddProductInShop(123, 60, allProduct, shop2);
        Product product21 = _shopManager.AddProductInShop(150, 60, allProduct1, shop2);
        Shop shopWithMinimalPrice = _shopManager.FindMinimalPrices(listPersonProduct);
        Assert.AreEqual(shopWithMinimalPrice.ShopId, shop1.ShopId);
    }
    
    [Test]
    public void BuyProducts_ProductsBought()
    {
        var vasya = new Person("vasya", 12000);
        AllProducts allProduct = _shopManager.AddInAllProduct("apple");
        AllProducts allProduct1 = _shopManager.AddInAllProduct("pear");
        PersonProduct personProduct = _shopManager.AddProductInBasket(allProduct, 4, vasya);
        PersonProduct personProduct1 = _shopManager.AddProductInBasket(allProduct1, 2, vasya);
        List<PersonProduct> listPersonProduct = _shopManager.GiveListBasket(vasya);
        Shop shop = _shopManager.AddShop("magnit", "dawdawda");
        Product product = _shopManager.AddProductInShop(130, 60, allProduct, shop);
        Product product1 = _shopManager.AddProductInShop(144, 60, allProduct1, shop);
        Shop shop1 = _shopManager.AddShop("pyaterochka", "dawdawda");
        Product product11 = _shopManager.AddProductInShop(153, 60, allProduct, shop1);
        Product product12 = _shopManager.AddProductInShop(103, 60, allProduct1, shop1);
        Shop shop2 = _shopManager.AddShop("perekrestok", "dawdawda");
        Product product2 = _shopManager.AddProductInShop(143, 60, allProduct, shop2);
        Product product21 = _shopManager.AddProductInShop(163, 60, allProduct1, shop2);
        _shopManager.BuyProducts(listPersonProduct, vasya);
        Assert.AreEqual(vasya.PersonWallet, 9346);
    }
     }
}