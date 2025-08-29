using AutoMapper;
using ContactManager.Application.Contracts.Persistance;
using ContactManager.Application.Features.Contacts.Queries.GetContactsList;
using ContactManager.Application.Profiles;
using ContactManager.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace ContactManager.Application.UnitTests.Contacts.Queries
{
    public class GetContactsListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IContactRepository> _mockContactRepository;

        public GetContactsListQueryHandlerTest()
        {
            _mockContactRepository = ContactRepositoryMock.GetContactRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetContactList_ReturnsCorrectCourseDetails()
        {
            var handler = new GetContactsListQueryHandler(_mockContactRepository.Object, _mapper);

            var result = await handler.Handle(new GetContactsListQuery() , CancellationToken.None);

            result.ShouldBeOfType<List<ContactsListVm>>();

            result.Count.ShouldBe(2);
        }
    }
}