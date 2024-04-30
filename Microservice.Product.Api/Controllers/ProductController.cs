using MediatR;
using Microservice.Product.Application.UseCases.Product.Commands.CreateCommand;
using Microservice.Product.Application.UseCases.Product.Commands.DeleteCommand;
using Microservice.Product.Application.UseCases.Product.Commands.UpdateCommand;
using Microservice.Product.Application.UseCases.Product.Queries.GetAllQuery;
using Microservice.Product.Application.UseCases.Product.Queries.GetByIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List")]
        public async Task<IActionResult> ProductList()
        {
            var response = await _mediator.Send(new GetAllProductQuery());

            return Ok(response);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> ProductById(int productId)
        {
            var response = await _mediator.Send(new GetProductByIdQuery() { ProductId = productId });

            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> ProductCreate([FromBody] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> ProductUpdate([FromBody] UpdateProductCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("Delete/{productId:int}")]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var response = await _mediator.Send(new DeleteProductCommand() { ProductId = productId});

            return Ok(response);
        }
    }
}