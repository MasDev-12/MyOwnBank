using FluentValidation;
using MyOwnBank.Database.EntityConfiguration.Extensions;
using MyOwnBank.Features.Users.Registrations;
using MyOwnBank.Pipeline;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMediatR(cnf =>
                cnf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
                   .AddOpenBehavior(typeof(ValidationBehavior<,>)));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.AddDbContext();
builder.AddUsers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
