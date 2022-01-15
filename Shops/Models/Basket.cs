using System.Collections.Generic;

namespace Shops.Models
{
    public class Basket
    {
        public Basket()
        {
            ProductsInBasket = new List<PersonProduct>();
        }

        public List<PersonProduct> ProductsInBasket { get; }
    }
}