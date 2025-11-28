using Shared.DataTransferObject.Category;
using Shared.DataTransferObject.Product;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICategoryService 
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges);
        Task<CategoryDto> GetByIdAsync(int id, bool trackChanges);
        Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetProductsByCategoryIdAsync(int categoryId, ProductParameters productParameters, bool trackChanges);

    }
}
