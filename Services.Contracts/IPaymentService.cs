using Shared.DataTransferObject.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IPaymentService
    {
        Task HandlePaymobCallbackAsync(PaymobCallbackDto dto);
    }
}
