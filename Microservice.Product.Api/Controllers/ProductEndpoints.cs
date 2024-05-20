using Carter;
using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Dtos.Product.Request;
using Microservice.Product.Application.Interfaces.Events;
using Microservice.Product.Application.UseCases.Product.Commands.CreateCommand;
using Microservice.Product.Application.UseCases.Product.Commands.DeleteCommand;
using Microservice.Product.Application.UseCases.Product.Commands.UpdateCommand;
using Microservice.Product.Application.UseCases.Product.Queries.GetAllQuery;
using Microservice.Product.Application.UseCases.Product.Queries.GetByIdQuery;
using Microservice.Product.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using YamlDotNet.Core.Tokens;

namespace Microservice.Product.Api.Controllers
{
    public class ProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeBase = app.MapGroup("api/products");

            app.MapGet("list", ProductList);
            app.MapGet("{productId:int}", ProductById);
            app.MapPost("create", ProductCreate);
            app.MapPost("event", ProductCreateEvent);
            app.MapPut("update", ProductUpdate);
            app.MapDelete("{productId:int}", ProductDelete);
        }

        public static async Task<IResult> ProductList(
            int numFilter,
            string textFilter,
            ISender sender)
        {
            var response = await sender.Send(new GetAllProductQuery()
            {
                NumFilter = numFilter,
                TextFilter = textFilter
            });
            return Results.Ok(response);
        }

        public static async Task<IResult> ProductById(
            int productId, 
            ISender sender)
        {
            var response = await sender.Send(new GetProductByIdQuery() { ProductId = productId });
            return Results.Ok(response);
        }

        public static async Task<IResult> ProductCreate(
            CreateProductCommand command,
            ISender sender)
        {
            var response = await sender.Send(command);
            return Results.Ok(response);
        }

        public static IResult ProductCreateEvent(
            ProductRequestDto command,
            IProductEventBus productEvent)
        {
            productEvent.OrderProduct(command);
            return Results.Ok(command);
        }

        public static async Task<IResult> ProductUpdate(
            UpdateProductCommand command,
            ISender sender)
        {
            var response = await sender.Send(command);
            return Results.Ok(response);
        }

        public static async Task<IResult> ProductDelete(
            int productId,
            ISender sender)
        {
            var response = await sender.Send(new DeleteProductCommand() { ProductId = productId});
            return Results.Ok(response);
        }
    }
}