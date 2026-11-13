
using BookingSystem.Application.DTOs;
using FluentValidation;

namespace BookingSystem.Application.Validators
{
    public class CreateBookingValidator:AbstractValidator<CreateBookingDTO>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.ApartmentId).NotEmpty().WithMessage("ApartmentId is required.");
            RuleFor(x => x.StartDate).GreaterThan(DateTime.UtcNow).WithMessage("StartDate must be in the future.");
            RuleFor(x => x.EndDate).GreaterThan(x=>x.StartDate).WithMessage("EndDate must be after StartDate.");

        }
    }
}
