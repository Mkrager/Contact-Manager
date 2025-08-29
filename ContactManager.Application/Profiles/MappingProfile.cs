using AutoMapper;
using ContactManager.Application.DTOs;
using ContactManager.Application.Features.Contacts.Queries.GetContactsList;
using ContactManager.Domain.Entites;

namespace ContactManager.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactCsvDto>().ReverseMap();
            CreateMap<Contact, ContactsListVm>().ReverseMap();
        }
    }
}