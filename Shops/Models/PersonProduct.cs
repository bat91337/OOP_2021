namespace Shops.Models
{
    public class PersonProduct
    {
        public PersonProduct(AllProduct product, int productAmount)
        {
            ProductAmount = productAmount;
            Product = product;
        }

        public int ProductAmount { get; }
        public AllProduct Product { get; }
    }
}