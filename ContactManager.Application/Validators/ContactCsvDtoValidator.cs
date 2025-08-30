using ContactManager.Application.DTOs;
using FluentValidation;

namespace ContactManager.Application.Validators
{
    public class ContactCsvDtoValidator : AbstractValidator<ContactCsvDto>
    {
        public ContactCsvDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(c => c.DateOfBirth)
                .GreaterThan(new DateTime(1753, 1, 1))
                .WithMessage("Date of birth must be after 1753-01-01.")
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{9,15}$").WithMessage("Phone number must be between 9 and 15 digits.");

            RuleFor(c => c.Salary)
                .GreaterThanOrEqualTo(0).WithMessage("Salary must be non-negative.");
        }
    }
}
