using AutoMapper;
using MemorySaver.Domain.Entities;
using MemorySaver.Domain.ServiceContracts.DTOs.Request;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;

namespace MemorySaver.Services.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserRequestDTO, User>();
            CreateMap<User, LoginUserResponseDTO>();
        }
    }
}
