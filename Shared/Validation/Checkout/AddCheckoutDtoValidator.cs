using FluentValidation;
using Shared.DataTransferObject.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validators.Checkout
{
    public class AddCheckoutDtoValidator : AbstractValidator<AddCheckoutDto>
    {
        public AddCheckoutDtoValidator()
        {
            RuleFor(c => c.DeliveryAddress)
                .NotEmpty().WithMessage("Delivery address is required.")
                .MaximumLength(200).WithMessage("Delivery address must not exceed 200 characters.");
            RuleFor(c => c.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .Must(method => new[] { "card", "cash" }.Contains(method))
                .WithMessage("Payment method must be one of the following: card, cash.");
        }
    }
}
