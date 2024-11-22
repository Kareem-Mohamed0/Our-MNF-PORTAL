using FluentValidation;
using MNF_PORTAL_Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNF_PORTAL_Service.Validators
{
    public class UserValidator : AbstractValidator<DetailsUserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Full_Name)
              .NotEmpty().WithMessage("Fullname is required")
              .MinimumLength(10).WithMessage("Fullname must be at least 10 characters long");

            RuleFor(x => x.User_Name)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

          
        }
    }
}
