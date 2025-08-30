using AutoMapper;
using ContactManager.Application.DTOs;
using ContactManager.Application.Features.Contacts.Commands.UpdateContact;
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

            CreateMap<Contact, UpdateContactCommand>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}