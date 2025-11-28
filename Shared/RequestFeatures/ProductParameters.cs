using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class ProductParameters : RequestParameters
    {
        public ProductParameters() => OrderBy = "Name";
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = 9999999999999999.99M;
        public bool ValidRange => MaxPrice > MinPrice;
        public string? SearchTerm { get; set; }
    }
}
