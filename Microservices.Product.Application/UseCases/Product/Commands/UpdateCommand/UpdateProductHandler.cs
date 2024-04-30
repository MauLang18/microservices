using AutoMapper;
using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Interfaces.Services;
using Entity = Microservice.Product.Domain.Entities;

namespace Microservice.Product.Application.UseCases.Product.Commands.UpdateCommand;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

            var product = _mapper.Map<Entity.Product>(request);
            product.Id = request.ProductId;
            _unitOfWork.Product.UpdateAsync(product);
            await _unitOfWork.SaveChangeAsync();

            response.IsSuccess = true;
            response.Message = "Actualización exitosa.";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}