using Contracts.IRepository;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
                await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();
        public async Task<Category?> GetCategoryAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateCategory(Category category) => Create(category);

        public void DeleteCategory(Category category) =>
            Delete(category);
    }
}
