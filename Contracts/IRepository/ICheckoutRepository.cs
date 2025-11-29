using Entities.Model;
using Shared.DataTransferObject.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{ 
    public interface ICheckoutRepository
    {
        Task<(Order order, IEnumerable<OrderItem> orderItems, int cartId)> ProcessCheckoutAsync(string userId, AddCheckoutDto checkout);
        Task CancelCheckoutAsync(int orderId, int cartId);
    }
}
