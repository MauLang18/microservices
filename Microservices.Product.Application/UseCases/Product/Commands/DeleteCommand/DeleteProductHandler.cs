using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Interfaces.Services;

namespace Microservice.Product.Application.UseCases.Product.Commands.DeleteCommand;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsProduct = await _unitOfWork.Product.GetByIdAsync(request.ProductId);

            if (existsProduct is null)
            {
                response.IsSuccess = false;
                response.Message = "El producto no existe en la base de datos.";
                return response;
            }

            await _unitOfWork.Product.DeleteAsync(request.ProductId);
            await _unitOfWork.SaveChangeAsync();

            response.IsSuccess = true;
            response.Message = "Eliminación exitosa.";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}