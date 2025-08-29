using AutoMapper;
using ContactManager.Application.Contracts.Infrastructure;
using ContactManager.Application.Contracts.Persistance;
using ContactManager.Application.DTOs;
using ContactManager.Domain.Entites;
using MediatR;

namespace ContactManager.Application.Features.Contacts.Commands.UploadContactCsv
{
    public class UploadContactCsvCommandHandler : IRequestHandler<UploadContactCsvCommand>
    {
        private readonly ICsvService<ContactCsvDto> _contactCsvService;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public UploadContactCsvCommandHandler(
            ICsvService<ContactCsvDto> contactCsvService, 
            IContactRepository contactRepository,
            IMapper mapper)
        {
            _contactCsvService = contactCsvService;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UploadContactCsvCommand request, CancellationToken cancellationToken)
        {
            var contacts = await _contactCsvService.ParseCsvAsync(request.FileStream);

            await _contactRepository.AddRangeAsync(_mapper.Map<List<Contact>>(contacts));

            return Unit.Value;
        }
    }
}