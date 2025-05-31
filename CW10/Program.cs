using CW10.Services;
using CW10.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApbdContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default-db"));
});

builder.Services.AddScoped<ITripService, TripService>();

var app = builder.Build();

app.MapControllers();

app.Run();