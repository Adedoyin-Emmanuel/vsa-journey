using Microsoft.AspNetCore.Identity;

namespace vsa_journey.Features.Authentication.Tokens;

public class CustomEmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public CustomEmailConfirmationTokenProviderOptions()
    {
        Name = nameof(CustomEmailConfirmationTokenProviderOptions);
        TokenLifespan = TimeSpan.FromDays(3);

    }   
}