using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public decimal Total { get; set; }
        public PaymentMethod payment { get; set; }
        public string DeliveryAddress { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> OrderItems { get; set; } 
    }
}
