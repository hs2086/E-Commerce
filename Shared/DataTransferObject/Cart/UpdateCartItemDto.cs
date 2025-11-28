using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Cart
{
    public class UpdateCartItemDto 
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }   
    }
}
