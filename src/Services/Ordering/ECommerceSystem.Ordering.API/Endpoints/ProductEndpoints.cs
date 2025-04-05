using Carter;
using ECommerceSystem.Application.Commands.CreateProduct;
using ECommerceSystem.Application.Commands.DeleteProduct;
using ECommerceSystem.Application.Queries.GetProducts;
using MediatR;

namespace ECommerceSystem.API.Endpoints
{
    public class ProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/products");

            group.MapGet("/", async (ISender sender) =>
            {
                var query = new GetProductsQuery();
                var result = await sender.Send(query);
                return Results.Ok(result);
            });

            group.MapPost("/", async (CreateProductCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Created($"/products/{result.Id}", result);
            });

            group.MapDelete("/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCommand(id);
                await sender.Send(command);
                return Results.NoContent();
            });
        }
    }
}
