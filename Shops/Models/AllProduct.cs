using Shops.Tools;
namespace Shops.Models
{
    public class AllProduct
    {
        public AllProduct(string productName, int productId)
        {
            ProductId = productId;
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