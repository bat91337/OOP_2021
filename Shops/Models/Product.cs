namespace Shops.Models
{
    public class Product
    {
        public Product(string name, decimal price, int amount, string shop)
        {
            NameProduct = name;
            Amount = amount;
            Price = price;
        }

        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string NameProduct { get; }
    }
    }
