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
    public class CreateProductDtoValidator : ProductForManipulationDtoValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
        }
    }
}
