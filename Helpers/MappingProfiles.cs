using AutoMapper;
using SimpleDotNETAPI.Models;
using SimpleDotNETAPI.DTO;

namespace SimpleDotNETAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<Application, ApplicationDTO>();
            CreateMap<ApplicationDTO, Application>();
            CreateMap<RedirectURI, RedirectURIDTO>();
            CreateMap<RedirectURIDTO, RedirectURI>();
        }
    }

}