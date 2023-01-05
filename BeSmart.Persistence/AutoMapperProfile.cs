using AutoMapper;
using BeSmart.Domain.Models;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.DTOs.Category;

namespace BeSmart.Persistence
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreationDTO>().ReverseMap();
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Answer, AnswerCreationDTO>().ReverseMap();
        }
    }
}
