using AutoMapper;
using Models.Entities;
using Models.Dtos;
namespace Models.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
	CreateMap<Product, ProductDto>();
	CreateMap<ProductDto, Product>();
    }
}
