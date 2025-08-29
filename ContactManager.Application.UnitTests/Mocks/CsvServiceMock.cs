using ContactManager.Application.Contracts.Infrastructure;
using ContactManager.Application.DTOs;
using Moq;

namespace ContactManager.Application.UnitTests.Mocks
{
    public class CsvServiceMock
    {
        public static Mock<ICsvService<ContactCsvDto>> GetCsvService()
        {
            var mockContacts = new List<ContactCsvDto>
            {
                new ContactCsvDto 
                { 
                    Name = "John Smith", 
                    DateOfBirth = new DateTime(1995, 4, 12), 
                    Married = false, 
                    Phone = "+380971234567", 
                    Salary = 18500.50m 
                },
                new ContactCsvDto 
                {
                    Name = "Olena Ivanova", 
                    DateOfBirth = new DateTime(1988, 11, 3), 
                    Married = true, 
                    Phone = "+380631112233", 
                    Salary = 25400.75m 
                }
            };

            var mockService = new Mock<ICsvService<ContactCsvDto>>();

            mockService.Setup(r => r.ParseCsvAsync(It.IsAny<Stream>()))
                .ReturnsAsync(mockContacts);

            return mockService;
        }
    }
}
