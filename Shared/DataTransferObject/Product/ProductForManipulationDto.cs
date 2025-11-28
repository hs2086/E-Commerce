using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Product
{
    public abstract record ProductForManipulationDto
    {
        public string Name { get; set; }    
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
