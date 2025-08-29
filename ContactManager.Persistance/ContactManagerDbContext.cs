using ContactManager.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Persistence
{
    public class ContactManagerDbContext : DbContext
    {
        public ContactManagerDbContext(DbContextOptions<ContactManagerDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
