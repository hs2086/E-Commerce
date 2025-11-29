using Contracts.IRepository;
using Entities.Exceptions;
using Repository.Data;
using Shared.DataTransferObject.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RepositoryContext repositoryContext;

        public PaymentRepository(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public async Task HandlePaymobCallbackAsync(PaymobCallbackDto dto)
        {
            var order = await repositoryContext.Orders.FindAsync(int.Parse(dto.order.merchant_order_id));
            if (order == null) 
                throw new OrderNotFoundException($"Order with ID {dto.order.merchant_order_id} not found.");

            if (dto.success)
                order.Status = Entities.Model.OrderStatus.Confirmed;
            else
                order.Status = Entities.Model.OrderStatus.Cancelled;
            await repositoryContext.SaveChangesAsync();
        }
    }
}
