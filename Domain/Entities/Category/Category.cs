namespace vsa_journey.Domain.Entities.Category;

public class Category
{
    public Guid Id { get; private set; }
    
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; private init; }
    
    public DateTime UpdatedAt { get; private init; }
}