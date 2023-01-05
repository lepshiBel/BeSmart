using AutoMapper;
using BeSmart.Domain.Models;
using BeSmart.Domain.DTOs;
using BeSmart.Domain.DTOs.Answer;

namespace BeSmart.Persistence
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreationDTO>().ReverseMap();
        }
    }
}
