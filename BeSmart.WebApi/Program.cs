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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BeSmart.Application;
using BeSmart.WebApi.Middleware;


var builder = WebApplication.CreateBuilder(args);
var key = SomeOptions.GenerateBytes();

builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(swagger =>
{
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<LessonCreationDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = SomeOptions.Issuer,
            ValidateAudience = false,
            ValidAudience = SomeOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
});


builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceAnswer, AnswerService>();
builder.Services.AddScoped<IServiceCategory, CategoryService>();
builder.Services.AddScoped<IServiceTest, TestService>();
builder.Services.AddScoped<IServiceQuestion, QuestionService>();
builder.Services.AddScoped<IServiceCard, CardService>();
builder.Services.AddScoped<IServiceLesson, LessonService>();
builder.Services.AddScoped<IServiceCourse, CourseService>();
builder.Services.AddScoped<IServiceTheme, ThemeService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseExceptionHandlerMiddleware();

app.UseSwagger();

app.MapControllers();

app.UseRouting();
app.UseMiddleware<TokenValidationMiddleware>();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Swagger";
});

app.Run();
