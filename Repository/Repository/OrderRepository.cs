using Contracts.IRepository;
using Entities.Exceptions;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Shared.DataTransferObject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RepositoryContext _repositoryContext;
        public OrderRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _repositoryContext.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _repositoryContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await GetOrderByIdAsync(id);
            if (order is null)
                throw new OrderNotFoundException($"Order with ID {id} not found.");
            order.Status = status;
            _repositoryContext.Orders.Update(order);
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task<(Order order, OrderItem orderItem)> MakeOrderAsync(AddOrderDto order, string userId)
        {
            var product = await _repositoryContext.Products.FirstOrDefaultAsync(p => p.Id == order.ProductId);
            if (product is null)
                throw new ProductNotFoundException($"Product with ID {order.ProductId} not found.");

            var newOrder = new Order()
            {
                UserId = userId,
                Total = product.Price * order.Quantity,
                payment = Enum.Parse<PaymentMethod>(order.PaymentMethod.ToLower(), true),
                DeliveryAddress = order.Address,
                Status = OrderStatus.Pending
            };

            await _repositoryContext.Orders.AddAsync(newOrder);
            await _repositoryContext.SaveChangesAsync();

            var orderItem = new OrderItem()
            {
                OrderId = newOrder.Id,
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = order.Quantity,
                UnitPrice = product.Price
            };
            await _repositoryContext.OrderItems.AddAsync(orderItem);
            await _repositoryContext.SaveChangesAsync();

            return (newOrder, orderItem);
        }

        public async Task<bool> CheckIfUserIsBuyTheProductWithSpecifiedIdAsync(string userId, int productId)
        {
            var orders = await _repositoryContext.Orders.Where(o => o.UserId == userId).ToListAsync();

            if (orders == null) return false;

            foreach (var order in orders)
            {
                var orderItem = await _repositoryContext.OrderItems.Where(oi => oi.ProductId == productId && oi.OrderId == order.Id).FirstOrDefaultAsync();
                if (orderItem != null) return true;
            }
            return false;
        }
    }
}
