using System.Collections.Generic;
using Shops.Models;
namespace Shops.Repositories
{
    public class ListAllProductRepository
    {
        private readonly List<AllProducts> _listproduct;

        public ListAllProductRepository()
        {
            _listproduct = new List<AllProducts>();
        }

        public List<AllProducts> GetListAllProduct()
        {
            return _listproduct;
        }

        public void AddAllProduct(AllProducts product)
        {
            _listproduct.Add(product);
        }
    }
}