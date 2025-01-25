namespace vsa_journey.Features.Products.GetProductById.Query;

public class GetProductByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public bool IsPublished { get; set; }
}