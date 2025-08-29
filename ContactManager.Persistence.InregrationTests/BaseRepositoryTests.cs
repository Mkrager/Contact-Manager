using ContactManager.Domain.Entites;
using ContactManager.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Persistence.InregrationTests
{
    public class BaseRepositoryTests
    {
        private readonly ContactManagerDbContext _dbContext;
        private readonly BaseRepository<Contact> _repository;

        public BaseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ContactManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactManagerDb")
                .Options;

            _dbContext = new ContactManagerDbContext(options);
            _repository = new BaseRepository<Contact>(_dbContext);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntityToDatabase()
        {
            var Contact = new Contact { Name = "New Contact", DateOfBirth = DateTime.UtcNow };

            var result = await _repository.AddAsync(Contact);

            var addedContact = await _dbContext.Contacts.FindAsync(result.Id);
            Assert.NotNull(addedContact);
            Assert.Equal("New Contact", addedContact.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            var contact = new Contact { Name = "Old Name", DateOfBirth = DateTime.UtcNow };
            await _repository.AddAsync(contact);

            contact.Name = "Updated Name";
            await _repository.UpdateAsync(contact);

            var updatedContact = await _dbContext.Contacts.FindAsync(contact.Id);
            Assert.NotNull(updatedContact);
            Assert.Equal("Updated Name", updatedContact.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity()
        {
            var contact = new Contact { Name = "Old Name", DateOfBirth = DateTime.UtcNow };
            await _repository.AddAsync(contact);

            await _repository.DeleteAsync(contact);

            var deletedContact = await _dbContext.Contacts.FindAsync(contact.Id);
            Assert.Null(deletedContact);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            var contact = new Contact { Name = "Old Name", DateOfBirth = DateTime.UtcNow };
            await _repository.AddAsync(contact);

            var result = await _repository.GetByIdAsync(contact.Id);

            Assert.NotNull(result);
            Assert.Equal(contact.Name, result.Name);
        }

        [Fact]
        public async Task ListAllAsync_ShouldReturnAllEntities()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
             
            var result = await _repository.ListAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}