using FluentValidation;
using Shared.DataTransferObject.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validators.Category
{
    public class CreateCategoryDtoValidator : CategoryForManipulationDtoValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
        }
    }
}
