using Entities.Model;
using Shared.DataTransferObject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository 
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task<Order> GetOrderByIdAsync(int id);
        Task UpdateOrderStatusAsync(int id, OrderStatus status);
        Task<(Order order, OrderItem orderItem)> MakeOrderAsync(AddOrderDto order, string userId);
        Task<bool> CheckIfUserIsBuyTheProductWithSpecifiedIdAsync(string userId, int productId);
    }
}
