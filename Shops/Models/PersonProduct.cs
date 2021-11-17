namespace Shops.Models
{
    public class PersonProduct
    {
        public PersonProduct(AllProducts products, int productAmount)
        {
            ProductAmount = productAmount;
            Products = products;
        }

        public int ProductAmount { get; }
        public AllProducts Products { get; }
    }
}