using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace vsa_journey.Features.Authentication.Tokens;

public class CustomForgotPasswordTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
{
    public CustomForgotPasswordTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<DataProtectionTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider, options, logger)
    {
    }
}