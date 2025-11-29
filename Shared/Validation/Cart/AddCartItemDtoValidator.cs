using FluentValidation;
using Shared.DataTransferObject.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validators.Cart
{
    public class AddCartItemDtoValidator : AbstractValidator<AddCartItemDto>
    {
        public AddCartItemDtoValidator()
        {
            RuleFor(c => c.Quantity).GreaterThan(0).WithMessage("Quantity cannot be negative or equal to zero.");
            RuleFor(c => c.ProductId).GreaterThan(0).WithMessage("Product id cannot be negative or equal to zero.");
        }
    }
}
