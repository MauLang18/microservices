using AutoMapper;
using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Interfaces.Services;
using Entity = Microservice.Product.Domain.Entities;

namespace Microservice.Product.Application.UseCases.Product.Commands.CreateCommand;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var product = _mapper.Map<Entity.Product>(request);
            await _unitOfWork.Product.CreateAsync(product);
            await _unitOfWork.SaveChangeAsync();

            response.IsSuccess = true;
            response.Message = "Registro exitoso.";
        }
        catch (Exception ex) 
        {
            response.Message = ex.Message;
        }

        return response;
    }
}