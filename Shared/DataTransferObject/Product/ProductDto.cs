using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Product
{
    public record ProductDto(
        int Id,
        string Name,
        string Description,
        decimal Price,
        int StockQuantity,
        string? ImagePath,
        int CategoryId
    );
}
