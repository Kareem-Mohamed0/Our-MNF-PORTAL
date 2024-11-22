using FluentValidation;
using MNF_PORTAL_Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Service.Validators
{
    public class RoleValidator : AbstractValidator<string>
    {
        public RoleValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("Role is required")
                .MinimumLength(3).WithMessage("Role must be at least 3 characters long");
        }
    }
}
