using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using Microservice.Payment.Api.Contracts.Products;
using Microservice.Payment.Api.Database;
using Microservice.Payment.Api.Entities;
using Microservice.Payment.Api.Shared.Bases;

namespace Microservice.Payment.Api.Features.Products;

public class CreateProduct
{
    #region Command
    public class Command : IRequest<BaseResponse<bool>>
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int StockMin { get; set; }
        public int StockMax { get; set; }
        public decimal UnitSalePrice { get; set; }
    }
    #endregion

    #region Validator
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Code)
            .NotNull().WithMessage("El Código no puede ser nulo.")
            .NotEmpty().WithMessage("El Código no puede ser vacio.")
            .MinimumLength(5).WithMessage("El Código debe tener almenos 5 caracteres.")
            .MaximumLength(10).WithMessage("El Código debe tener como máximo 10 caracteres.");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("El Nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El Nombre no puede ser vacio.");

            RuleFor(x => x.StockMin)
                .NotNull().WithMessage("El Stock Mínimo no puede ser nulo.")
                .NotEmpty().WithMessage("El Stock Mínimo no puede ser vacio.");

            RuleFor(x => x.StockMax)
                .NotNull().WithMessage("El Stock Máximo no puede ser nulo.")
                .NotEmpty().WithMessage("El Stock Máximo no puede ser vacio.");
        }
    }
    #endregion

    #region Handler
    internal sealed class Handler : IRequestHandler<Command, BaseResponse<bool>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(ApplicationDbContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<BaseResponse<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = "Errores de validación.";
                return response;
            }

            var product = new Product
            {
                Code = request.Code,
                Name = request.Name,
                StockMin = request.StockMin,
                StockMax = request.StockMax,
                UnitSalePrice = request.UnitSalePrice,
            };

            _context.Add(product);
            
            var recordsAffected = await _context.SaveChangesAsync(cancellationToken);

            response.IsSuccess = recordsAffected > 0;
            response.Message = response.IsSuccess ? "Registrado correctamente." : "Registro fallido.";
            return response;
        }
    }
    #endregion

    #region Endpoint
    public class CreateProductEnpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeBase = app.MapGroup("api/payment/products");

            app.MapPost("create", async(CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<Command>();
                var response = await sender.Send(command);
                return Results.Ok(response);
            });
        }
    }
    #endregion
}