using AutoMapper;
using ContactManager.Application.Contracts.Persistance;
using ContactManager.Application.Features.Contacts.Commands.UpdateContact;
using ContactManager.Application.Profiles;
using ContactManager.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace ContactManager.Application.UnitTests.Contacts.Commands
{
    public class UpdateContactCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IContactRepository> _mockCourseRepository;

        public UpdateContactCommandTests()
        {
            _mockCourseRepository = ContactRepositoryMock.GetContactRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task UpdateCourse_ValidCommand_UpdatesCourseSuccessfully()
        {
            var handler = new UpdateContactCommandHandler(_mockCourseRepository.Object, _mapper);
            var updateCommand = new UpdateContactCommand
            {
                Id = Guid.Parse("103204ea-e2db-40ad-b645-4440a9856e31"),
                DateOfBirth = new DateTime(2025, 12, 12),
                Salary = 1234,
                Married = false,
                Name = "New",
                Phone = "+41234233423"
            };

            await handler.Handle(updateCommand, CancellationToken.None);

            var updatedContact = await _mockCourseRepository.Object.GetByIdAsync(updateCommand.Id);

            updatedContact.ShouldNotBeNull();
            updatedContact.Married.ShouldBe(updateCommand.Married);
            updatedContact.Name.ShouldBe(updateCommand.Name);
            updatedContact.Phone.ShouldBe(updateCommand.Phone);
            updatedContact.Salary.ShouldBe(updateCommand.Salary);
            updatedContact.DateOfBirth.ShouldBe(updateCommand.DateOfBirth);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenNameEmpty()
        {
            var validator = new UpdateContactCommandValidator();
            var query = new UpdateContactCommand
            {
                Id = Guid.Parse("103204ea-e2db-40ad-b645-4440a9856e31"),
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
            var validator = new UpdateContactCommandValidator();
            var query = new UpdateContactCommand
            {
                Id = Guid.Parse("103204ea-e2db-40ad-b645-4440a9856e31"),
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
            var validator = new UpdateContactCommandValidator();
            var query = new UpdateContactCommand
            {
                Id = Guid.Parse("103204ea-e2db-40ad-b645-4440a9856e31"),
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
            var validator = new UpdateContactCommandValidator();
            var query = new UpdateContactCommand
            {
                Id = Guid.Parse("103204ea-e2db-40ad-b645-4440a9856e31"),
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
