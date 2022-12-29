using BeSmart.Application.Interfaces;
using BeSmart.Application.Service;
using BeSmart.Domain.Interfaces;
using BeSmart.Persistence;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using BeSmart.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using BeSmart.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<AnswerValidator>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceAnswer, AnswerService>();
builder.Services.AddScoped<IServiceQuestion, QuestionService>();

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Swagger";
});

app.Run();
