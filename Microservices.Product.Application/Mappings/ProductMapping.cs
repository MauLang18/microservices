using AutoMapper;
using Microservice.Product.Application.Dtos.Product.Response;
using Microservice.Product.Application.UseCases.Product.Commands.CreateCommand;
using Microservice.Product.Application.UseCases.Product.Commands.UpdateCommand;
using Entity = Microservice.Product.Domain.Entities;

namespace Microservice.Product.Application.Mappings;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Entity.Product, ProductResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateProduct, x => x.MapFrom(y => y.State == 1 ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<Entity.Product, ProductByIdResponseDto>()
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateProductCommand, Entity.Product>();

        CreateMap<UpdateProductCommand, Entity.Product>();
    }
}