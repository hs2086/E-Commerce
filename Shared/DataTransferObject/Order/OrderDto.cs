using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public decimal Total { get; set; }
        public string DeliveryAddress { get; set; }
        public string Status { get; set; }
    }
}
