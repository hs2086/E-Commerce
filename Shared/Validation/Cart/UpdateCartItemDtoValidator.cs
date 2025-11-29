using FluentValidation;
using Shared.DataTransferObject.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validators.Cart
{
    public class UpdateCartItemDtoValidator : AbstractValidator<UpdateCartItemDto>
    {
        public UpdateCartItemDtoValidator() 
        {
            RuleFor(c => c.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");
            RuleFor(c => c.ProductId).GreaterThan(0).WithMessage("Product id cannot be negativ or equal to zero.");
        }
    }
}
