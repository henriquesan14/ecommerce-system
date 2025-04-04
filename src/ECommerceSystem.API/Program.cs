using Carter;
using ECommerceSystem.API.Extensions;
using ECommerceSystem.Application.Commands.CreateOrder;
using ECommerceSystem.Application.EventHandlers.Integration;
using ECommerceSystem.Application.Validators;
using ECommerceSystem.EventBus.Extensions;
using ECommerceSystem.Infrastructure;
using ECommerceSystem.Shared.Behaviors;
using ECommerceSystem.Shared.Exceptions.Handler;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureDatabase(builder.Configuration);

builder.Services.AddJsonSerializationConfig();

// Add services to the container.
builder.Services.AddCarter();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureRepositories();
builder.Services.AddRedis(builder.Configuration);


builder.Services.AddMessageBroker(builder.Configuration, typeof(OrderCreatedIntegrationEventHandler).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(CreateOrderCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateOrderCommandValidator).Assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecksConfig(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.Run();
