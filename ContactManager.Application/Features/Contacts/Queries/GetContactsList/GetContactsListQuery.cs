using MediatR;

namespace ContactManager.Application.Features.Contacts.Queries.GetContactsList
{
    public class GetContactsListQuery : IRequest<List<ContactsListVm>>
    {
    }
}
