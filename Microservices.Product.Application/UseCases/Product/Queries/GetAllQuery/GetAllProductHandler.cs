using AutoMapper;
using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Dtos.Product.Response;
using Microservice.Product.Application.Interfaces.Services;

namespace Microservice.Product.Application.UseCases.Product.Queries.GetAllQuery;

public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, BaseResponse<IEnumerable<ProductResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProductResponseDto>>();

        try
        {
            var products = await _unitOfWork.Product.GetAllAsync();

            if(products is null) 
            {
                response.IsSuccess = false;
                response.Message = "No se encontraron registros.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            response.Message = "Consulta exitosa.";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}