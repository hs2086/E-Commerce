using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository 
{
    public interface IPaymentRepository
    {
        Task HandlePaymobCallbackAsync(Shared.DataTransferObject.Payments.PaymobCallbackDto dto);
    }
}
