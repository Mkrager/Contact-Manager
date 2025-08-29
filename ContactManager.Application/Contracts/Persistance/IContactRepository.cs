using ContactManager.Domain.Entites;

namespace ContactManager.Application.Contracts.Persistance
{
    public interface IContactRepository : IAsyncRepository<Contact>
    {
        Task AddRangeAsync(List<Contact> contacts);
    }
}
