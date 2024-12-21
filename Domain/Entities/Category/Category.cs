namespace vsa_journey.Domain.Entities.Category;

public class Category
{
    public Guid Id { get; private set; }
    
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}