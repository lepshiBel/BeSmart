using AutoMapper;
using BeSmart.Server.Domain.DTOs;
using BeSmart.Server.Domain.Models;

namespace BeSmart.Server.Persistence
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<User, UserLoginRequestDTO>().ReverseMap();
            CreateMap<User, UserLoginResponseDTO>().ReverseMap();
        }
    }
}
