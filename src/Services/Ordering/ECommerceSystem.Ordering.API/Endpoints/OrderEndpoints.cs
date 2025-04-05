using Carter;
using ECommerceSystem.Application.Commands.CreateOrder;
using ECommerceSystem.Application.Queries.GetOrders;
using MediatR;

namespace ECommerceSystem.API.Endpoints
{
    public class OrderEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/orders");

            group.MapPost("/", async (CreateOrderCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);

                return Results.Created($"orders/{result.Id}", result);
            });

            group.MapGet("/", async (ISender sender) =>
            {
                var query = new GetOrdersQuery();
                var result = await sender.Send(query);

                return Results.Ok(result);
            });
        }
    }
}
