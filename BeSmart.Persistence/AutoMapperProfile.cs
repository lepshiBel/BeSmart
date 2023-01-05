using AutoMapper;
using BeSmart.Domain.Models;
using BeSmart.Domain.DTOs;

namespace BeSmart.Persistence
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>();
        }
    }
}
