using AutoMapper;
using ContactManager.Application.Contracts.Persistance;
using ContactManager.Application.Exceptions;
using ContactManager.Domain.Entites;
using MediatR;

namespace ContactManager.Application.Features.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
    {
        private readonly IAsyncRepository<Contact> _contactRepository;
        private readonly IMapper _mapper;
        public UpdateContactCommandHandler(IAsyncRepository<Contact> contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contactToUpdate = await _contactRepository.GetByIdAsync(request.Id);

            if (contactToUpdate == null)
                throw new NotFoundException(nameof(Contact), request.Id);

            _mapper.Map(request, contactToUpdate, typeof(UpdateContactCommand), typeof(Contact));

            await _contactRepository.UpdateAsync(contactToUpdate);

            return Unit.Value;
        }
    }
}
