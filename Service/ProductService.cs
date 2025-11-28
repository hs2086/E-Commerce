using AutoMapper;
using Contracts;
using Contracts.IRepository;
using Contracts.Logger;
using Entities.Exceptions;
using Entities.Model;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using Shared.DataTransferObject.Product;
using Shared.RequestFeatures;
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
    }
}
