using System.Collections.Generic;

namespace Shops.Models
{
    public class Shop
    {
        public Shop(string name, string address, int id)
        {
            NameShop = name;
            Address = address;
            Id = id;
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public string NameShop { get; }
        public string Address { get; }

        public int Id { get; }
    }
}