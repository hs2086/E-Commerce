using AutoMapper;
using Contracts;
using Contracts.IRepository;
using Contracts.Logger;
using Entities.Exceptions;
using Entities.Model;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using Shared.DataTransferObject.Product;
using Shared.RequestFeatures;
using Shared.Validators.Product;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IDataShaper<ProductDto> dataShaper;

        public ProductService(
            IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper,
            IConfiguration configuration,
            IDataShaper<ProductDto> dataShaper)
        {
            this.repositoryManager = repositoryManager;
            this.logger = logger;
            this.mapper = mapper;
            this.configuration = configuration;
            this.dataShaper = dataShaper;
        }

        public async Task<(IEnumerable<ExpandoObject> products, MetaData metaData)> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges)
        {
            if (!productParameters.ValidRange)
                throw new MaxPriceRangeBadRequestException();


            var productsWithMetaData = await repositoryManager.Product.GetAllProductsAsync(productParameters, trackChanges);
            var productsDto = mapper.Map<IEnumerable<ProductDto>>(productsWithMetaData);

            var shapedProducts = dataShaper.ShapeData(productsDto, productParameters.Fields);

            return (products: shapedProducts, metaData: productsWithMetaData.MetaData);
        }
        private async Task<Product> GetProductAndCheckIfExistAsync(int id, bool trackChanges)
        {
            var product = await repositoryManager.Product.GetProductAsync(id, trackChanges);
            if (product == null)
                throw new ProductNotFoundException($"Product with id: {id} doesn't exist in the database.");
            return product;
        }
        private async Task<Category> GetCategoryAndCheckIfExistAsync(int id, bool trackChanges)
        {
            var category = await repositoryManager.Category.GetCategoryAsync(id, trackChanges);
            if (category == null)
                throw new CategoryNotFoundException(id);
            return category;
        }
        public async Task<ProductDto?> GetProductByIdAsync(int id, bool trackChanges)
        {
            var product = await GetProductAndCheckIfExistAsync(id, trackChanges);
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }
        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string query, bool trackChanges)
        {
            var products = await repositoryManager.Product.SearchProductsAsync(query, trackChanges);
            if (!products.Any())
                throw new ProductNotFoundException($"There is no products match this {query}");
            var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }
        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var validation = new CreateProductDtoValidator();
            validation.ValidateAndThrow(createProductDto);

            await GetCategoryAndCheckIfExistAsync(createProductDto.CategoryId, false);
            var extension = Path.GetExtension(createProductDto.Image.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine("wwwroot/Images/Products/", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await createProductDto.Image.CopyToAsync(stream);
            }
            filePath = filePath.Replace("wwwroot", string.Empty);

            var product = mapper.Map<Product>(createProductDto);
            product.ImagePath = filePath;
            repositoryManager.Product.CreateProduct(product);
            await repositoryManager.SaveAsync();

            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }
        public async Task UpdateProductAsync(int id, UpdateProductDto updateProductDto, bool trackChanges)
        {
            var validation = new UpdateProductDtoValidator();
            validation.ValidateAndThrow(updateProductDto);

            // check if the category is found
            await GetCategoryAndCheckIfExistAsync(updateProductDto.CategoryId, false);

            // check if the product is found
            var productDb = await GetProductAndCheckIfExistAsync(id, trackChanges);

            // delete the old image 
            if (System.IO.File.Exists($"wwwroot/{productDb.ImagePath}"))
            {
                System.IO.File.Delete($"wwwroot/{productDb.ImagePath}");
            }

            // save the new image 
            var extension = Path.GetExtension(updateProductDto.Image.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine("wwwroot/Images/Products/", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await updateProductDto.Image.CopyToAsync(stream);
            }
            filePath = filePath.Replace("wwwroot", string.Empty);

            mapper.Map(updateProductDto, productDb);
            productDb.ImagePath = filePath;

            //var updatedProduct = mapper.Map<Product>(updateProductDto);
            //repositoryManager.Product.Update(updatedProduct);
            await repositoryManager.SaveAsync();
        }
        public async Task DeleteProductAsync(int id, bool trackChange)
        {
            var product = await GetProductAndCheckIfExistAsync(id, trackChange);

            if (System.IO.File.Exists($"wwwroot/{product.ImagePath}"))
            {
                System.IO.File.Delete($"wwwroot/{product.ImagePath}");
            }

            repositoryManager.Product.DeleteProduct(product);
            await repositoryManager.SaveAsync();
        }
    }
}
