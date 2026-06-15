using PetHealth.Infrastructure.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Récupération de la connection strings dans les user-secrets
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbConnection(connection ?? throw new ArgumentNullException());

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();