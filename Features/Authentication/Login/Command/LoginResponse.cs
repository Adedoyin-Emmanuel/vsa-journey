namespace vsa_journey.Features.Authentication.Login.Commands;

public sealed record LoginResponse
{
    public string AccessToken { get; set; } 
    
    public string RefreshToken { get; set; }
}