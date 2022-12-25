using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
