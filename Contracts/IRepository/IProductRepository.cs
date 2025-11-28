using Entities.Model;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges);
        Task<Product?> GetProductAsync(int id, bool trackChanges);
        Task<IEnumerable<Product>> SearchProductsAsync(string query, bool trackChanges);
        Task<PagedList<Product>> GetProductsByCategoryIdAsync(int categoryId, ProductParameters productParameters, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
