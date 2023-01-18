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
    swagger.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        Description = "ouath",
        Name = "oauth2.0",
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"https://accounts.google.com/o/oauth2/v2/auth"),
                TokenUrl = new Uri($"https://www.googleapis.com/oauth2/v4/token"),

                Scopes = new Dictionary<string, string>
                {
                    {
                        $"https://www.googleapis.com/auth/cloud-platform.read-only",
                        "User"
                    }

                }
            }
        }
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },

                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<LessonCreationDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(config =>
//    {
//        config.RequireHttpsMetadata = false;
//        config.SaveToken = true;
//        config.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = false,
//            ValidIssuer = SomeOptions.Issuer,
//            ValidateAudience = false,
//            ValidAudience = SomeOptions.Audience,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(key)
//        };
//    });

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = "1093291377089-eu4kbk7loa9tsvhdf1jrpubk2spgoqj7.apps.googleusercontent.com";
        options.ClientSecret = "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";
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
    options.OAuthClientId("1093291377089-eu4kbk7loa9tsvhdf1jrpubk2spgoqj7.apps.googleusercontent.com");
    options.OAuthClientSecret("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");   
    options.OAuthScopes("email");
    options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
});

app.Run();
