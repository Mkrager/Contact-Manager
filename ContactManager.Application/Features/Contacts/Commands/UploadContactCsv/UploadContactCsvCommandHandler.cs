using AutoMapper;
using ContactManager.Application.Contracts.Infrastructure;
using ContactManager.Application.Contracts.Persistance;
using ContactManager.Application.DTOs;
using ContactManager.Domain.Entites;
using MediatR;

namespace ContactManager.Application.Features.Contacts.Commands.UploadContactCsv
{
    public class UploadContactCsvCommandHandler : IRequestHandler<UploadContactCsvCommand, Guid>
    {
        private readonly ICsvService<ContactCsvDto> _contactCsvService;
        private readonly IAsyncRepository<Contact> _contactRepository;
        private readonly IMapper _mapper;
        public UploadContactCsvCommandHandler(
            ICsvService<ContactCsvDto> contactCsvService, 
            IAsyncRepository<Contact> contactRepository,
            IMapper mapper)
        {
            _contactCsvService = contactCsvService;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UploadContactCsvCommand request, CancellationToken cancellationToken)
        {
            var contacts = await _contactCsvService.ParseCsvAsync(request.FileStream);

            var result = await _contactRepository.AddAsync(_mapper.Map<List<Contact>>(contacts).First());

            return result.Id;
        }
    }
}