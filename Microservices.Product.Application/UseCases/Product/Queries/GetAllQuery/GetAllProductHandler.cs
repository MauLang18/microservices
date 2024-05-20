using AutoMapper;
using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Dtos.Product.Response;
using Microservice.Product.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Product.Application.UseCases.Product.Queries.GetAllQuery;

public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, BaseResponse<IEnumerable<ProductResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProductResponseDto>>();

        try
        {
            var products = _unitOfWork.Product.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch(request.NumFilter)
                {
                    case 1:
                        products = products.Where(x => x.Code.Contains(request.TextFilter));
                        break;
                    case 2:
                        products = products.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                products = products.Where(x => x.State.Equals(request.StateFilter));
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                products = products.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                               x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, products).ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await products.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(items);
            response.Message = "Consulta exitosa.";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}