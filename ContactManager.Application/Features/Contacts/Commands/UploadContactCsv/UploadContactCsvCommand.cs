using MediatR;

namespace ContactManager.Application.Features.Contacts.Commands.UploadContactCsv
{
    public class UploadContactCsvCommand : IRequest<Guid>
    {
        public Stream FileStream { get; set; } = default!;
    }
}
