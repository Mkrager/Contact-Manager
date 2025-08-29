using AutoMapper;
using ContactManager.Application.Contracts.Persistance;
using ContactManager.Application.Features.Contacts.Commands.DeleteContact;
using ContactManager.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace ContactManager.Application.UnitTests.Contacts.Commands
{
    public class DeleteContactCommandTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;

        public DeleteContactCommandTests()
        {
            _mockContactRepository = ContactRepositoryMock.GetContactRepository();
        }

        [Fact]
        public async Task Delete_Contact_RemovesContactFromRepository()
        {
            var handler = new DeleteContactCommandHandler(_mockContactRepository.Object);
            await handler.Handle(new DeleteContactCommand() 
            { 
                Id = Guid.Parse("103204ea-e2db-40ad-b645-4440a9856e31") 
            }, CancellationToken.None);

            var allCourses = await _mockContactRepository.Object.ListAllAsync();
            allCourses.Count.ShouldBe(1);
        }

    }
}
