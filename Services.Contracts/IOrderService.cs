using Entities.Model;
using Shared.DataTransferObject.Checkout;
using Shared.DataTransferObject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task UpdateOrderStatusAsync(int id, OrderStatus status);
        Task<(CheckoutDto checkout1, string url)> MakeOrderAsync(AddOrderDto order, string userId);
    }
}
