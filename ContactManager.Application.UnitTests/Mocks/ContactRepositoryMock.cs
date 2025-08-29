using ContactManager.Application.Contracts.Persistance;
using ContactManager.Domain.Entites;
using EmptyFiles;
using Moq;

namespace ContactManager.Application.UnitTests.Mocks
{
    public class ContactRepositoryMock
    {
        public static Mock<IContactRepository> GetContactRepository()
        {
            var contacts = new List<Contact>
            {
                new Contact
                {
                    Id = Guid.Parse("103204ea-e2db-40ad-b645-4440a9856e31"),
                    Name = "John Smith",
                    DateOfBirth = new DateTime(1995, 4, 12),
                    Married = false,
                    Phone = "+380971234567",
                    Salary = 18500.50m
                },
                new Contact
                {
                    Id = Guid.Parse("5ea3c588-c5fd-455a-9b80-7534617ef8b5"),
                    Name = "Olena Ivanova",
                    DateOfBirth = new DateTime(1988, 11, 3),
                    Married = true,
                    Phone = "+380631112233",
                    Salary = 25400.75m
                }
            };

            var mockRepository = new Mock<IContactRepository>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(contacts);

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Contact>()))
                .Callback((Contact contact) => contacts.Remove(contact));

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => contacts.FirstOrDefault(x => x.Id == id));


            mockRepository.Setup(r => r.AddRangeAsync(It.IsAny<List<Contact>>()))
                .Callback((List<Contact> contacts) =>
                {
                    contacts.AddRange(contacts);
                });

            return mockRepository;
        }
    }
}
