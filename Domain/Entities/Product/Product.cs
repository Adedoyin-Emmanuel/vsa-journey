namespace vsa_journey.Domain.Entities.Product;

public class Product
{
    public Guid Id { get; private init; }
    
    public string Name { get; private init; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }

    public bool IsPublished { get; set; } = true;

    public ProductStatus Status { get; set; } = ProductStatus.Available;
    
    public string BaseImageUrl { get; set; }
    
    public ICollection<string> Images { get; set; }
    
    public ICollection<string> Tags { get; set; }
    
    public Category.Category Category { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public int Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}