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
using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.DTOs.Membership;
using BeSmart.Domain.DTOs.StatusTheme;
using BeSmart.Domain.DTOs.StatusLesson;
using BeSmart.Domain.DTOs.StatusTest;

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
            CreateMap<Test, TestWithQuestionsDTO>().ForMember(d => d.QuestionsWithAnswers, s => s.MapFrom(s => s.Questsions)).ReverseMap();

            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Answer, AnswerCreationDTO>().ReverseMap();
            CreateMap<Answer, AnswerUpdateDTO>().ReverseMap();

            CreateMap<Question, QuestionCreationDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Question, QuestionWithAnswersDTO>().ForMember(d => d.Answers, s => s.MapFrom((s => s.Answers))).ReverseMap();

            CreateMap<Lesson, LessonDTO>().ReverseMap();
            CreateMap<Lesson, LessonCreationDTO>().ReverseMap();
            CreateMap<Lesson, LessonWithCardsDTO>().ForMember(d => d.Cards, s => s.MapFrom(s => s.Cards)).ReverseMap();

            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Course, CourseCreationDTO>().ReverseMap();
            CreateMap<Course, CourseUpdateDTO>().ReverseMap();
            CreateMap<Course, CourseWithThemesDTO>().ForMember(d => d.Themes, s => s.MapFrom((s => s.CourseThemes))).ReverseMap();

            CreateMap<Theme, ThemeDTO>().ReverseMap();
            CreateMap<Theme, ThemeCreationDTO>().ReverseMap();
            CreateMap<Theme, ThemeWithLessonsDTO>().ForMember(d => d.Lessons, s => s.MapFrom((s => s.Lessons))).ReverseMap();
            CreateMap<Theme, ThemeWithTestsDTO>().ForMember(d => d.Tests, s => s.MapFrom((s => s.Tests))).ReverseMap();

            CreateMap<User, UserLoginRequestDTO>().ReverseMap();

            CreateMap<Membership, MembershipDTO>().ForMember(d => d.NameOfCourse, s => s.MapFrom((s => s.Course.Name)))
                .ForMember(d=>d.CountOfThemes, s => s.MapFrom(s => s.Course.CountOfThemes))
                .ForMember(d=>d.NameOfCourseCategory, s=>s.MapFrom(s=>s.Course.Category.Name))
                .ReverseMap();

            CreateMap<Membership, MembershipWithThemesDTO>().ForMember(d => d.MembershipStatus, s => s.MapFrom((s => s.Status)))
                .ForMember(d => d.CourseName, s => s.MapFrom(s => s.Course.Name))
                .ForMember(d => d.CourseCountThemes, s => s.MapFrom(s => s.Course.CountOfThemes))
                .ReverseMap();

            CreateMap<StatusTheme, StatusThemeWithThemeDTO>().ForMember(d => d.Id, s => s.MapFrom((s => s.Id)))
                .ForMember(d => d.Status, s => s.MapFrom(s => s.Status))
                .ForMember(d => d.Theme, s => s.MapFrom(s => s.Theme))
                .ReverseMap();


            CreateMap<StatusLesson, StatusLessonWithLessonDTO>().ForMember(d => d.Id, s => s.MapFrom((s => s.Id)))
                .ForMember(d => d.StatusLesson, s => s.MapFrom(s => s.Status))
                .ForMember(d => d.Lesson, s => s.MapFrom(s => s.Lesson))
                .ReverseMap();

            //CreateMap<StatusTheme, StatusThemeWithLessonsDTO>().ForMember(d => d.Id, s => s.MapFrom((s => s.Id)))
            //    .ForMember(d => d.StatusTheme, s => s.MapFrom(s => s.Status))
            //    .ForMember(d => d.NameOfTheme, s => s.MapFrom(s => s.Theme.Name))
            //    .ForMember(d => d.CountOfLessons, s => s.MapFrom(s => s.Theme.CountLesson))
            //    .ForMember(d => d.StatusLessonsWithLessons, s => s.MapFrom(s => s.StatusLessons))
            //    .ReverseMap();

            CreateMap<StatusThemeWithLessons, StatusThemeWithLessonsDTO>().ForMember(d => d.Id, s => s.MapFrom((s => s.Id)))
                //.ForMember(d => d.StatusTheme, s => s.MapFrom(s => s.StatusTheme))
                .ForMember(d => d.NameOfTheme, s => s.MapFrom(s => s.NameOfTheme))
                .ForMember(d => d.CountOfLessons, s => s.MapFrom(s => s.CountOfLessons))
                .ForMember(d => d.StatusLessonsWithLessons, s => s.MapFrom(s => s.StatusLessons))
                .ReverseMap();

            CreateMap<StatusTest, StatusTestDTO>().ForMember(d => d.TestStatus, s => s.MapFrom(s => s.Status))
                .ForMember(d => d.TestName, s => s.MapFrom(s => s.Test.Name))
                .ForMember(d => d.AmountOfIncorrect, s => s.MapFrom(s => s.AmountOfIncorrectAnswers))
                .ForMember(d => d.AmountOfFaithfull, s => s.MapFrom(s => s.AmountOfFaithfullAnswers))
                .ReverseMap();
        }
    }
}
