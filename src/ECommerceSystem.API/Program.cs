using ECommerceSystem.API.Extensions;
using ECommerceSystem.Application.Commands.CreateOrder;
using ECommerceSystem.Application.Validators;
using ECommerceSystem.EventBus;
using ECommerceSystem.Infrastructure.Persistence;
using ECommerceSystem.Shared.Behaviors;
using ECommerceSystem.Shared.Exceptions.Handler;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<ECommerceSystemContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure();

builder.Services.AddJsonSerializationConfig();

builder.Services.AddEventBus(builder.Configuration);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(CreateOrderCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateOrderCommandValidator).Assembly);

builder.Services.AddAutoMapperConfig();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecksConfig(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler(options => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.Run();
