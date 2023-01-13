using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.Interfaces;
using BeSmart.Persistence.Repositories;
using FluentValidation;
using BeSmart.WebApi.Extensions;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using BeSmart.Persistence;
using BeSmart.Application.Validators.Lesson;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<LessonCreationDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceAnswer, AnswerService>();
builder.Services.AddScoped<IServiceCategory, CategoryService>();
builder.Services.AddScoped<IServiceTest, TestService>();
builder.Services.AddScoped<IServiceQuestion, QuestionService>();
builder.Services.AddScoped<IServiceCard, CardService>();
builder.Services.AddScoped<IServiceLesson, LessonService>();
builder.Services.AddScoped<IServiceCourse, CourseService>();
builder.Services.AddScoped<IServiceTheme, ThemeService>();

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseSwagger();

app.MapControllers();

app.UseRouting();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Swagger";
});

app.Run();
