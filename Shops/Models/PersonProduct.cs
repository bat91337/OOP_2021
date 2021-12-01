namespace Shops.Models
{
    public class PersonProduct
    {
        public PersonProduct(Product products, int productAmount)
        {
            ProductAmount = productAmount;
            Products = products;
        }

        public int ProductAmount { get; }
        public Product Products { get; }
    }
}