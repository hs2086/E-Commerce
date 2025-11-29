using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Order
{
    public class AddOrderDto
    {
        public int ProductId { get; set; }  
        public int Quantity { get; set; }
        public string PaymentMethod { get; set; }
        public string Address { get; set; } 
    }
}
