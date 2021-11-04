using Shops.Tools;
namespace Shops.Models
{
    public class Product
    {
        public Product(AllProduct product, decimal price, int amount)
        {
            if (amount == 0)
            {
                throw new ShopsException("the amount cannot be zero");
            }

            ProductAmount = amount;
            if (price == 0)
            {
                throw new ShopsException("the price cannot be zero");
            }

            ProductPrice = price;
            Products = product;
        }

        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public AllProduct Products { get; }
    }
}
