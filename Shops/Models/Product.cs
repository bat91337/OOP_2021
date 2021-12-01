using Shops.Tools;
namespace Shops.Models
{
    public class Product
    {
        private static int _id;
        public Product(string productName)
        {
            ProductId = _id++;
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new ShopsException("empty line");
            }

            ProductName = productName;
        }

        public int ProductId { get; }
        public string ProductName { get; }
    }
}