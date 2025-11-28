using Contracts.IRepository;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Extensions;
using Repository.Repos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges)
        {
            var products = await FindAll(trackChanges)
                .Filter(productParameters.MinPrice, productParameters.MaxPrice)
                .Search(productParameters.SearchTerm)
                .Sort(productParameters.OrderBy)
                .ToListAsync();

            return PagedList<Product>.ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<PagedList<Product>> GetProductsByCategoryIdAsync(int categoryId, ProductParameters productParameters, bool trackChanges)
        {
            var products = await FindByCondition(p => p.CategoryId == categoryId, trackChanges)
                .Sort(productParameters.OrderBy)
                .ToListAsync();

            return PagedList<Product>.ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Product?> GetProductAsync(int id, bool trackChanges) =>
            await FindByCondition(p => p.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
        public async Task<IEnumerable<Product>> SearchProductsAsync(string query, bool trackChanges) =>
            await FindByCondition(p => p.Name.Contains(query) || p.Description.Contains(query), trackChanges)
            .ToListAsync();
        public void CreateProduct(Product product) => Create(product);
        public void DeleteProduct(Product product) => Delete(product);
    }
}
