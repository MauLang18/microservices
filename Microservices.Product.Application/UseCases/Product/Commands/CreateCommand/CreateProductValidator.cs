using FluentValidation;

namespace Microservice.Product.Application.UseCases.Product.Commands.CreateCommand;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
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