namespace Shops.Models
{
    public class PersonProduct
    {
        public PersonProduct(UniversumProducts products, int productAmount)
        {
            ProductAmount = productAmount;
            Products = products;
        }

        public int ProductAmount { get; }
        public UniversumProducts Products { get; }
    }
}