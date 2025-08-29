using ContactManager.Domain.Entites;
using ContactManager.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Persistence.InregrationTests
{
    public class ContactRepositoryTests
    {
        private readonly ContactManagerDbContext _dbContext;
        private readonly ContactRepository _repository;

        public ContactRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ContactManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactManagerDb")
                .Options;

            _dbContext = new ContactManagerDbContext(options);
            _repository = new ContactRepository(_dbContext);
        }

        [Fact]
        public async Task AddRangeAsync_ShouldAddEntityToDatabase()
        {
            var contact = new List<Contact>
            {
                new Contact 
                { 
                    Id = Guid.Parse("02781d68-de06-417d-a7db-659c8fc3fcb5"),
                    Name = "New Contact", 
                    DateOfBirth = 
                    DateTime.UtcNow 
                },                
                new Contact 
                {
                    Id = Guid.Parse("55443417-7d84-4894-be63-fab46cab66da"),
                    Name = "New Contact2", 
                    DateOfBirth = 
                    DateTime.UtcNow 
                },
            };

            await _repository.AddRangeAsync(contact);

            var addedContact = await _dbContext.Contacts.FindAsync(contact.First().Id);
            Assert.NotNull(addedContact);
            Assert.Equal("New Contact", addedContact.Name);
        }
    }
}