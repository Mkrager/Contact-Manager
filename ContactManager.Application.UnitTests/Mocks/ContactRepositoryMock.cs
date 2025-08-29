using ContactManager.Application.Contracts.Persistance;
using ContactManager.Domain.Entites;
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
                    Id = Guid.NewGuid(),
                    Name = "John Smith",
                    DateOfBirth = new DateTime(1995, 4, 12),
                    Married = false,
                    Phone = "+380971234567",
                    Salary = 18500.50m
                },
                new Contact
                {
                    Id = Guid.NewGuid(),
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

            mockRepository.Setup(r => r.AddRangeAsync(It.IsAny<List<Contact>>()))
                .Callback((List<Contact> contacts) =>
                {
                    contacts.AddRange(contacts);
                });

            return mockRepository;
        }
    }
}
