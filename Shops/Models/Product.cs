using Shops.Tools;
namespace Shops.Models
{
    public class Product
    {
        public Product(AllProducts products, decimal price, int amount)
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
            Productses = products;
        }

        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public AllProducts Productses { get; }
    }
}
