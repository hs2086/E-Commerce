using AutoMapper;
using Contracts.IRepository;
using Contracts.Logger;
using Entities.Exceptions;
using Entities.Model;
using FluentValidation;
using Services.Contracts;
using Shared.DataTransferObject.Category;
using Shared.DataTransferObject.Product;
using Shared.RequestFeatures;
using Shared.Validators.Category;
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
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var validation = new CreateCategoryDtoValidator();
            validation.ValidateAndThrow(categoryDto);

            var category = mapper.Map<Category>(categoryDto);
            repositoryManager.Category.CreateCategory(category);
            await repositoryManager.SaveAsync();
            var categorydto = mapper.Map<CategoryDto>(category);
            return categorydto;
        }
        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto, bool trackChanges)
        {
            var validation = new UpdateCategoryDtoValidator();
            validation.ValidateAndThrow(categoryDto);

            var categoryFromDb = await GetCategoryAndCheckIfExistAsync(id, trackChanges);
            mapper.Map(categoryDto, categoryFromDb);
            await repositoryManager.SaveAsync();
        }
        public async Task DeleteCategoryAsync(int id, bool trackChanges)
        {
            var category = await GetCategoryAndCheckIfExistAsync(id, trackChanges);
            repositoryManager.Category.DeleteCategory(category);
            await repositoryManager.SaveAsync();
        }
    }
}
