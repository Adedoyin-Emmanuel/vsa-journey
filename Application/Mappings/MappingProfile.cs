using AutoMapper;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Features.Authentication.Signup.Commands;

namespace vsa_journey.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SignupCommand, User>();
    }
}