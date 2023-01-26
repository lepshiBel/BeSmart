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
    // AuthorizationUrl = "https://accounts.google.com/o/oauth2/v2/auth"
    // TokenUrl = "https://www.googleapis.com/oauth2/v4/token"
    // ClientId = "1093291377089-eu4kbk7loa9tsvhdf1jrpubk2spgoqj7.apps.googleusercontent.com"
    // ClientSecret = "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"

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
    options.AddPolicy("user", policy => policy.RequireRole("user"));
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
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IServiceMembership, MembershipService>();
builder.Services.AddScoped<IServiceStatusTheme, StatusThemeService>();
builder.Services.AddScoped<IServiceStatusLesson, StatusLessonService>();
builder.Services.AddScoped<IServiceStatusTest, StatusTestService>();

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseExceptionHandlerMiddleware();

app.UseSwagger();
app.UseDeveloperExceptionPage();

app.MapControllers();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TokenValidationMiddleware>();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Swagger";
});

app.Run();
