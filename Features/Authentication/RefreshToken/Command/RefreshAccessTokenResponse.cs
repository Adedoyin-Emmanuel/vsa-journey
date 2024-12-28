namespace vsa_journey.Features.Authentication.RefreshToken.Command;

public class RefreshAccessTokenResponse
{
    public string AccessToken { get; set; } 
    
    public string RefreshToken { get; set; }
}