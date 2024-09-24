using DAL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Validation
{
    internal class RegisterRequestValidator : AbstractValidator<RegisterReq>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters at least.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.");

            // Compare Password and ConfirmPassword
            RuleFor(x => x).Must(x => x.Password == x.ConfirmPassword)
                .WithMessage("Password and Confirm Password must match.");
        }
    }
}
