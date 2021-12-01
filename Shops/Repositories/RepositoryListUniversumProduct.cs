using System.Collections.Generic;
using Shops.Models;
namespace Shops.Repositories
{
    public class RepositoryListUniversumProduct
    {
        private readonly List<UniversumProducts> _listProduct;

        public RepositoryListUniversumProduct()
        {
            _listProduct = new List<UniversumProducts>();
        }

        public List<UniversumProducts> GetListAllProduct()
        {
            return _listProduct;
        }

        public void AddAllProduct(UniversumProducts product)
        {
            _listProduct.Add(product);
        }
    }
}