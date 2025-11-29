using Shared.DataTransferObject.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICheckoutService
    {
        Task<(CheckoutDto checkout1, string url)> ProcessCheckoutAsync(string userId, AddCheckoutDto checkout);
    }
}
