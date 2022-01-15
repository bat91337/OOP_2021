using System.Collections.Generic;
using Shops.Models;
namespace Shops.Repositories
{
    public class ProductRepository
    {
        private readonly List<Product> _listProduct;

        public ProductRepository()
        {
            _listProduct = new List<Product>();
        }

        public List<Product> GetListAllProduct()
        {
            return _listProduct;
        }

        public void AddAllProduct(Product product)
        {
            _listProduct.Add(product);
        }
    }
}