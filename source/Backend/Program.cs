using Microsoft.EntityFrameworkCore;
using netCoreWebApi.Services;
using netCoreWebApi.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
