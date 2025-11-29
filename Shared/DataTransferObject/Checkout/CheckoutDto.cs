using Shared.DataTransferObject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Checkout
{
    public class CheckoutDto
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }  
        public decimal Price { get; set; }
        public decimal Delivery { get; set; }
        public string DeliveryAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> Items { get; set; }   
    }
}
