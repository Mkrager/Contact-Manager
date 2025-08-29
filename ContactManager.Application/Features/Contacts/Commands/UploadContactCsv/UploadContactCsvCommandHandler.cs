using ContactManager.Application.Contracts.Infrastructure;
using ContactManager.Application.Contracts.Persistance;
using ContactManager.Domain.Entites;
using MediatR;

namespace ContactManager.Application.Features.Contacts.Commands.UploadContactCsv
{
    public class UploadContactCsvCommandHandler : IRequestHandler<UploadContactCsvCommand, Guid>
    {
        private readonly ICsvService<Contact> _contactCsvService;
        private readonly IAsyncRepository<Contact> _contactRepository;
        public UploadContactCsvCommandHandler(ICsvService<Contact> contactCsvService, IAsyncRepository<Contact> contactRepository)
        {
            _contactCsvService = contactCsvService;
            _contactRepository = contactRepository;
        }

        public async Task<Guid> Handle(UploadContactCsvCommand request, CancellationToken cancellationToken)
        {
            var contacts = await _contactCsvService.ParseCsvAsync(request.FileStream);

            _contactRepository.AddAsync(contacts);
        }
    }
}