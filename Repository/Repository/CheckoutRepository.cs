using Contracts.IRepository;
using Entities.Exceptions;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Shared.DataTransferObject.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly RepositoryContext repositoryContext;

        public CheckoutRepository(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public async Task CancelCheckoutAsync(int orderId, int cartId)
        {
            var order = await repositoryContext.Orders.FindAsync(orderId);
            repositoryContext.Remove(order);
            var cart = await repositoryContext.Carts.FindAsync(cartId);
            cart.IsActive = true;
            await repositoryContext.SaveChangesAsync();
        }

        public async Task<(Order order, IEnumerable<OrderItem> orderItems, int cartId)> ProcessCheckoutAsync(string userId, AddCheckoutDto checkout)
        {
            var cart = await repositoryContext.Carts
                .Where(c => c.UserId == userId && c.IsActive)
                .FirstOrDefaultAsync();
            if (cart == null)
                throw new CartNotFoundException("Cart not found for the user.");

            var cartItems = await repositoryContext.CartItems.Where(ci => ci.CartId == cart.Id).ToListAsync();

            var order = new Order
            {
                UserId = userId,
                Total = cartItems.Sum(ci => ci.Quantity * ci.UnitPrice),
                payment = Enum.Parse<PaymentMethod>(checkout.PaymentMethod.ToLower()),
                DeliveryAddress = checkout.DeliveryAddress,
                Status = OrderStatus.Pending
            };
            await repositoryContext.Orders.AddAsync(order);
            await repositoryContext.SaveChangesAsync();

            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in cartItems)
            {
                orderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }
            await repositoryContext.OrderItems.AddRangeAsync(orderItems);
            cart.IsActive = false;
            await repositoryContext.SaveChangesAsync();

            return (order, orderItems, cart.Id);
        }
    }
}
