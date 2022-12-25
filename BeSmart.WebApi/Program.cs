using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Persistence;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddDbContext<BeSmartDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("default");
    options.UseSqlite(connectionString, opt =>
    {
        opt.MigrationsAssembly("BeSmart.WebApi");
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
