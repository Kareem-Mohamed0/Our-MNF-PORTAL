using FluentValidation;
using MNF_PORTAL_Service.DTOs;

namespace MNF_PORTAL_Service.Validators
{
    public class UserValidator : AbstractValidator<DetailsUserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
              .NotEmpty().WithMessage("FirstName is required")
              .MinimumLength(3).WithMessage("FirstName must be at least 3 characters long");

            RuleFor(x => x.LastName)
              .NotEmpty().WithMessage("LastName is required")
              .MinimumLength(3).WithMessage("LastName must be at least 3 characters long");

            RuleFor(x => x.User_Name)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");


        }
    }
}
