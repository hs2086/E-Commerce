using FluentValidation;
using Shared.DataTransferObject.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validators.Category
{
    public class CategoryForManipulationDtoValidator<T> : AbstractValidator<T> where T : CategoryForManipulationDto
    {
        public CategoryForManipulationDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.")
                .MinimumLength(3).WithMessage("Category name must be at least 3 characters.");

            RuleFor(c => c.Description)
                .NotNull().WithMessage("Category description is required.")
                .MinimumLength(5).WithMessage("Category description must be at least 5 characters.")
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
        }
    }
}
