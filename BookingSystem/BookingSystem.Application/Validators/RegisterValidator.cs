using FluentValidation;
using BookingSystem.Application.DTOs;

namespace BookingSystem.Application.Validators
{
    public class RegisterValidator:AbstractValidator<RegisterDTO>
    {
        private const int MinPasswordLength = 8;
        private const int MaxPasswordLength = 100;
        private const int MaxNameLength = 100;
        private const int MaxEmailLength = 256;
        public RegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters.")
                .MaximumLength(MaxNameLength).WithMessage($"Name must not exceed {MaxNameLength} characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.")
                .MaximumLength(MaxEmailLength).WithMessage($"Email must not exceed {MaxEmailLength} characters.");
           
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(MinPasswordLength).WithMessage($"Password must be at least {MinPasswordLength} characters.")
                .MaximumLength(MaxPasswordLength).WithMessage($"Password must not exceed {MaxPasswordLength} characters.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.");
        }
    }
}
