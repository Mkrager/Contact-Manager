using MediatR;

namespace ContactManager.Application.Features.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
