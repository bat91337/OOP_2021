using Shops.Tools;
namespace Shops.Models
{
    public class AllProducts
    {
        private static int _id;
        public AllProducts(string productName)
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