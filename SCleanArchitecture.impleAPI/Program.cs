using Microsoft.EntityFrameworkCore;
using SCleanArchitecture.SimpleAPI.Application.Extensions;
using SCleanArchitecture.SimpleAPI.Infrastructure.Data;
using SCleanArchitecture.SimpleAPI.Infrastructure.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// Your existing service registrations
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();