using ContactManager.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Persistence
{
    public class ContactManagerDbContext : DbContext
    {
        public ContactManagerDbContext(DbContextOptions<ContactManagerDbContext> options)
            : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
            new Contact
            {
                Id = Guid.Parse("cf140332-bddc-424e-8550-fd65bee52e78"),
                Name = "John Smith",
                DateOfBirth = new DateTime(1995, 4, 12),
                Married = false,
                Phone = "+380971234567",
                Salary = 18500.50m
            },
            new Contact
            {
                Id = Guid.Parse("5dc6b268-ea3c-4220-bdeb-e14beb48366c"),
                Name = "Olena Ivanova",
                DateOfBirth = new DateTime(1988, 11, 3),
                Married = true,
                Phone = "+380631112233",
                Salary = 25400.75m
            });
        }
    }
}
