
using BookingSystem.Application.DTOs;
using FluentValidation;

namespace BookingSystem.Application.Validators
{
    public class LoginValidator: AbstractValidator<LoginDTO>
    {
        private const int MaxEmailLength = 256;
        private const int MinPasswordLength = 8;
        private const int MaxPasswordLength = 100;
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.")
                .MaximumLength(MaxEmailLength).WithMessage($"Email must not exceed {MaxEmailLength} characters.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(MinPasswordLength).WithMessage($"Password must be at least {MinPasswordLength} characters.")
                .MaximumLength(MaxPasswordLength).WithMessage($"Password must not exceed {MaxPasswordLength} characters.");
        }
    }
}
