using MediatR;

namespace ContactManager.Application.Features.Contacts.Commands.UploadContactCsv
{
    public class UploadContactCsvCommand : IRequest
    {
        public Stream FileStream { get; set; } = default!;
    }
}
