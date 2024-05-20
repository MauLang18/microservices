using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Commons.Exceptions;
using Microservice.Product.Application.UseCases.Product.Commands.CreateCommand;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Product.Tests.Product.Commands;

[TestClass]
public class CreateProductCommandTest
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    [TestMethod]
    public async Task ShouldGetValidationErrors()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateProductCommand()
        {
            Code = "",
            Name = "",
            StockMin = 0,
            StockMax = 100,
            UnitSalePrice = 150000,
            State = 1
        };

        var expected = false;
        BaseResponse<bool> response = new();

        try
        {
            response = await mediator.Send(command);
            Assert.Fail(response.Message);
        }
        catch(ValidationException ex)
        {
            Assert.IsNotNull(ex.Errors);
            Assert.AreEqual(expected, response.IsSuccess);
        }
    }

    [TestMethod]
    public async Task ShouldCreateProduct()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var command = new CreateProductCommand()
        {
            Code = "PROD30",
            Name = "TV Smart",
            StockMin = 1,
            StockMax = 500,
            UnitSalePrice = 550000,
            State = 1
        };

        var expected = true;

        var response = await mediator.Send(command);

        Assert.AreEqual(expected, response.IsSuccess);
    }
}