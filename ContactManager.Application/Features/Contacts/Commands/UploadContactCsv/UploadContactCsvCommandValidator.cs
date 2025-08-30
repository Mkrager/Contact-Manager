using FluentValidation;

namespace ContactManager.Application.Features.Contacts.Commands.UploadContactCsv
{
    public class UploadContactCsvCommandValidator : AbstractValidator<UploadContactCsvCommand>
    {
        public UploadContactCsvCommandValidator()
        {
            RuleFor(r => r.FileStream)
                .NotNull().WithMessage("File is required.");

            RuleFor(r => r.FileStream)
                .Must(stream => stream != null && stream.Length > 0)
                .WithMessage("File cannot be empty.");
        }
    }
}
