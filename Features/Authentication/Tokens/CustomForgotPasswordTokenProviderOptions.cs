using Microsoft.AspNetCore.Identity;

namespace vsa_journey.Features.Authentication.Tokens;

public class CustomForgotPasswordTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public CustomForgotPasswordTokenProviderOptions()
    {
        Name = nameof(CustomForgotPasswordTokenProviderOptions);
        TokenLifespan = TimeSpan.FromHours(1);
    }
    
}