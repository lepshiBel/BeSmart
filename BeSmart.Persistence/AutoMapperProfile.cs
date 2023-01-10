using AutoMapper;
using BeSmart.Domain.Models;
using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.DTOs.Category;
using BeSmart.Domain.DTOs;
using BeSmart.Domain.DTOs.Question;
using BeSmart.Domain.DTOs.Card;
using BeSmart.Domain.DTOs.Lesson;
using BeSmart.Domain.DTOs.Course;
using BeSmart.Domain.DTOs.Theme;

namespace BeSmart.Persistence
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreationDTO>().ReverseMap();

            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<Card, CardCreationDTO>().ReverseMap();
            CreateMap<Card, CardUpdateDTO>().ReverseMap();

            CreateMap<Test, TestDTO>().ReverseMap();
            CreateMap<Test, TestCreationDTO>().ReverseMap();
            CreateMap<Test, TestWithQuestionsDTO>().ForMember(d => d.Questions, s => s.MapFrom(s => s.Questsions));

            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Answer, AnswerCreationDTO>().ReverseMap();
            CreateMap<Answer, AnswerUpdateDTO>().ReverseMap();

            CreateMap<Question, QuestionCreationDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Question, QuestionUpdateDTO>().ReverseMap();
            CreateMap<Question, QuestionWithAnswersDTO>().ForMember(d => d.Answers, s => s.MapFrom((s => s.Answers)));

            CreateMap<Lesson, LessonDTO>().ReverseMap();
            CreateMap<Lesson, LessonCreationDTO>().ReverseMap();
            CreateMap<Lesson, LessonWithCardsDTO>().ForMember(d => d.Cards, s => s.MapFrom(s => s.Cards));

            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Course, CourseCreationDTO>().ReverseMap();
            CreateMap<Course, CourseUpdateDTO>().ReverseMap();
            CreateMap<Course, CourseWithThemesDTO>().ForMember(d => d.Themes, s => s.MapFrom((s => s.CourseThemes)));

            CreateMap<Theme, ThemeDTO>().ReverseMap();
            CreateMap<Theme, ThemeCreationDTO>().ReverseMap();
            CreateMap<Theme, ThemeUpdateDTO>().ReverseMap();
            CreateMap<Theme, ThemeWithLessonsDTO>().ForMember(d => d.Lessons, s => s.MapFrom((s => s.Lessons)));
            CreateMap<Theme, ThemeWithTestsDTO>().ForMember(d => d.Tests, s => s.MapFrom((s => s.Tests)));
        }
    }
}
