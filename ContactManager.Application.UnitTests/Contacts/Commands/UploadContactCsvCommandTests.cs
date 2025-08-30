using AutoMapper;
using ContactManager.Application.Contracts.Infrastructure;
using ContactManager.Application.Contracts.Persistance;
using ContactManager.Application.DTOs;
using ContactManager.Application.Features.Contacts.Commands.UpdateContact;
using ContactManager.Application.Features.Contacts.Commands.UploadContactCsv;
using ContactManager.Application.Profiles;
using ContactManager.Application.UnitTests.Mocks;
using ContactManager.Application.Validators;
using Moq;
using Shouldly;

namespace ContactManager.Application.UnitTests.Contacts.Commands
{
    public class UploadContactCsvCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly Mock<ICsvService<ContactCsvDto>> _mockCsvService;

        public UploadContactCsvCommandTests()
        {
            _mockContactRepository = ContactRepositoryMock.GetContactRepository();
            _mockCsvService = CsvServiceMock.GetCsvService();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task UpdateCourse_ValidCommand_UpdatesCourseSuccessfully()
        {
            var handler = new UploadContactCsvCommandHandler(_mockCsvService.Object, _mockContactRepository.Object, _mapper);
            var uploadContactCommand = new UploadContactCsvCommand
            {
                FileStream = new MemoryStream()
            };

            await handler.Handle(uploadContactCommand, CancellationToken.None);

            var contacts = await _mockContactRepository.Object.ListAllAsync();

            var firstContact = contacts.First();

            firstContact.Name.ShouldBe("John Smith");
            firstContact.Phone.ShouldBe("+380971234567");
            firstContact.Salary.ShouldBe(18500.50m);
        }

        [Fact]
        public async Task Validator_ShouldHaveError_WhenFileNull()
        {
            var validator = new UploadContactCsvCommandValidator();
            var query = new UploadContactCsvCommand
            {
                FileStream = null
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors,
                f => f.PropertyName == nameof(UploadContactCsvCommand.FileStream)
                  && f.ErrorMessage == "File is required.");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenNameEmpty()
        {
            var validator = new ContactCsvDtoValidator();
            var query = new ContactCsvDto
            {
                DateOfBirth = new DateTime(2025, 12, 12),
                Salary = 1234,
                Married = false,
                Name = "",
                Phone = "+41234233423"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Name");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenIncorrectDate()
        {
            var validator = new ContactCsvDtoValidator();
            var query = new ContactCsvDto
            {
                DateOfBirth = new DateTime(1752, 1, 1),
                Salary = 1234,
                Married = false,
                Name = "Name",
                Phone = "+41234233423"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "DateOfBirth");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenNegativeSalary()
        {
            var validator = new ContactCsvDtoValidator();
            var query = new ContactCsvDto
            {
                DateOfBirth = new DateTime(2025, 12, 12),
                Salary = -1234,
                Married = false,
                Name = "Name",
                Phone = "+41234233423"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Salary");
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenIncorrectNumber()
        {
            var validator = new ContactCsvDtoValidator();
            var query = new ContactCsvDto
            {
                DateOfBirth = new DateTime(2025, 12, 12),
                Salary = -1234,
                Married = false,
                Name = "Name",
                Phone = "+41234233423412341242345235235235235235234"
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Phone");
        }

    }
}