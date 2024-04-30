using AutoMapper;
using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Dtos.Product.Response;
using Microservice.Product.Application.Interfaces.Services;

namespace Microservice.Product.Application.UseCases.Product.Queries.GetByIdQuery;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, BaseResponse<ProductByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProductByIdResponseDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ProductByIdResponseDto>();

        try
        {
            var product = await _unitOfWork.Product.GetByIdAsync(request.ProductId);

            if (product is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontró el producto.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<ProductByIdResponseDto>(product);
            response.Message = "Consulta exitosa.";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}