using AutoMapper;
using BeSmart.Domain.Models;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.DTOs.Category;
using BeSmart.Domain.DTOs;
using BeSmart.Domain.DTOs.Question;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.DTOs.Category;
using BeSmart.Domain.DTOs.Question;

namespace BeSmart.Persistence
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreationDTO>().ReverseMap();

            CreateMap<Test, TestDTO>().ReverseMap();
            CreateMap<Test, TestCreationDTO>().ReverseMap();
            CreateMap<Test, TestWithQuestionsDTO>().ReverseMap();

            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Answer, AnswerCreationDTO>().ReverseMap();

            CreateMap<Question, QuestionCreationDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Question, QuestionWithAnswersDTO>().ReverseMap();
        }
    }
}
