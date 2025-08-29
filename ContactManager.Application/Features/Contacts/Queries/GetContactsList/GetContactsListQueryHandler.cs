using AutoMapper;
using ContactManager.Application.Contracts.Persistance;
using ContactManager.Domain.Entites;
using MediatR;

namespace ContactManager.Application.Features.Contacts.Queries.GetContactsList
{
    public class GetContactsListQueryHandler : IRequestHandler<GetContactsListQuery, List<ContactsListVm>>
    {
        private readonly IAsyncRepository<Contact> _contactRepository;
        private readonly IMapper _mapper;

        public GetContactsListQueryHandler(IAsyncRepository<Contact> contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<List<ContactsListVm>> Handle(GetContactsListQuery request, CancellationToken cancellationToken)
        {
            var allContacts = await _contactRepository.ListAllAsync();
            return _mapper.Map<List<ContactsListVm>>(allContacts);
        }
    }
}
