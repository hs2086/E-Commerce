using AutoMapper;
using Contracts.IRepository;
using Contracts.Logger;
using Entities.Exceptions;
using Entities.Model;
using Services.Contracts;
using Shared.DataTransferObject.Category;
using Shared.DataTransferObject.Product;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public CategoryService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges)
        {
            var categories = await repositoryManager.Category.GetAllCategoriesAsync(trackChanges);

            if (!categories.Any())
                throw new CategoryNotFoundException("No categories found in the database.");

            var categoriesDto = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }

        private async Task<Category> GetCategoryAndCheckIfExistAsync(int id, bool trackChanges)
        {
            var category = await repositoryManager.Category.GetCategoryAsync(id, trackChanges);
            if (category == null)
                throw new CategoryNotFoundException(id);
            return category;
        }

        public async Task<CategoryDto> GetByIdAsync(int id, bool trachChanges)
        {
            var category = await GetCategoryAndCheckIfExistAsync(id, trachChanges);
            var categoryDto = mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
        public async Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetProductsByCategoryIdAsync(int categoryId, ProductParameters productParameters, bool trackChanges)
        {
            var category = await GetCategoryAndCheckIfExistAsync(categoryId, trackChanges);
            var products = await repositoryManager.Product.GetProductsByCategoryIdAsync(categoryId, productParameters, trackChanges);
            var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
            return (products: productsDto, metaData: products.MetaData);
        }

    }
}
