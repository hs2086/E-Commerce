using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObject.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validators.Product
{
    public class ProductForManipulationDtoValidator<T> : AbstractValidator<T> where T : ProductForManipulationDto
    {
        public ProductForManipulationDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Product name is required.")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");


            RuleFor(x => x.Description)
                .NotNull().WithMessage("Product Description is required.")
                .MinimumLength(5).WithMessage("Product Description must be at least 5 characters.")
                .MaximumLength(500).WithMessage("Product Description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product Price must be greater than 0.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Product Stock quantity cannot be negative.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category Id must be greater than 0.");

            RuleFor(x => x.Image)
                .NotNull().WithMessage("Product image is required.")
                .Must(BeAValidImage).WithMessage("Only image files are allowed.\nOnly this extensions are allowed ( \".jpg\", \".jpeg\", \".png\", \".webp\" )");
        }


        private bool BeAValidImage(IFormFile file)
        {
            if (file == null)
                return false;

            string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return permittedExtensions.Contains(extension);
        }
    }
}
