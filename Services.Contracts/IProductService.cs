using Shared.DataTransferObject.Product;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IProductService
    {
        Task<(IEnumerable<ExpandoObject> products, MetaData metaData)> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges);
        Task<ProductDto?> GetProductByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string query, bool trackChanges);
    }
}
