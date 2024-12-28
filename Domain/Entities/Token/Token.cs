using vsa_journey.Domain.Entities.Category;

namespace vsa_journey.Domain.Entities.Token;

public class Token : IBase
{
    public Guid Id { get; set; }
    
    public string Value { get; set; }

    public bool IsRevoked { get; set; } = false;
    
    public Guid UserId { get; set; }
    
    public User.User User { get; set; }  
    
    public TokenType Type { get; set; }
    
    public DateTime ExpiresAt { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}
