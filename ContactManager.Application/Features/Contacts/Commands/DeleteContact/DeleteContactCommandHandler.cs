using ContactManager.Application.Contracts.Persistance;
using ContactManager.Application.Exceptions;
using ContactManager.Domain.Entites;
using MediatR;

namespace ContactManager.Application.Features.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
    {
        private readonly IAsyncRepository<Contact> _contactRepository;
        public DeleteContactCommandHandler(IAsyncRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contactToDelete = await _contactRepository.GetByIdAsync(request.Id);

            if (contactToDelete == null)
                throw new NotFoundException(nameof(Contact), request.Id);

            await _contactRepository.DeleteAsync(contactToDelete);

            return Unit.Value;
        }
    }
}
