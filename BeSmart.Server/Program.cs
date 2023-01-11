using BeSmart.Server.Application.Interfaces;
using BeSmart.Server.Application.Services;
using BeSmart.Server.Domain.Interfaces;
using BeSmart.Server.Persistence;
using BeSmart.Server.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidIssuer = Options.Issuer,
        ValidateAudience = false,
        ValidAudience = Options.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = Options.GetSymmetricSecurityKey()
    };
});


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();
