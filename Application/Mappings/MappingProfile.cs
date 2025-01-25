using AutoMapper;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Domain.Entities.Product;
using vsa_journey.Features.Authentication.Signup.Commands;
using vsa_journey.Features.Products.CreateProduct.Command;
using vsa_journey.Features.Products.GetProductById.Query;

namespace vsa_journey.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SignupCommand, User>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreateProductResponse>().ForMember(dest => dest.Status,
            opt => opt.MapFrom(src => src.Status.ToString().ToLower()));
        CreateMap<Product, GetProductByIdResponse>().ForMember(dest => dest.Status,
            opt => opt.MapFrom(src => src.Status.ToString().ToLower()));
    }
}