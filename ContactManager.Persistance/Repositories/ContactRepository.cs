using ContactManager.Application.Contracts.Persistance;
using ContactManager.Domain.Entites;

namespace ContactManager.Persistence.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ContactManagerDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task AddRangeAsync(List<Contact> contacts)
        {
            await _dbContext.Contacts.AddRangeAsync(contacts);
            await _dbContext.SaveChangesAsync();
        }
    }
}