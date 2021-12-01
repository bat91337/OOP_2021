using Shops.Tools;
namespace Shops.Models
{
    public class ShopProduct
    {
        public ShopProduct(Product products, decimal price, int amount)
        {
            if (amount <= 0)
            {
                throw new ShopsException("the amount cannot be zero");
            }

            ProductAmount = amount;
            if (price <= 0)
            {
                throw new ShopsException("the price cannot be zero");
            }

            ProductPrice = price;
            Products = products;
        }

        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public Product Products { get; }
    }
}
