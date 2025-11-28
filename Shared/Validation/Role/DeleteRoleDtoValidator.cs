using FluentValidation;
using Shared.DataTransferObject.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validators.Role
{
    public class DeleteRoleDtoValidator : AbstractValidator<DeleteRoleDto>
    {
        public DeleteRoleDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(256).WithMessage("Role name must not exceed 256 characters.")
                .MinimumLength(3).WithMessage("Role name must be at least 3 characters long.");
        }
    }
}
