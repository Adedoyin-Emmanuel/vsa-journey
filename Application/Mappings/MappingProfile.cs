using AutoMapper;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Domain.Entities.Product;
using vsa_journey.Features.Products.CreateProduct;
using vsa_journey.Features.Authentication.Signup.Commands;

namespace vsa_journey.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SignupCommand, User>();
        CreateMap<CreateProductCommand, Product>();
    }
}