namespace vsa_journey.Domain.Entities.Order;

public class Order
{
    public Guid Id { get; private init; }
    
    public Guid UserId { get; private init; }
    
    public User.User User { get; private init; }
    
    public DateTime CreatedAt { get; private init; }
    
    public DateTime UpdatedAt { get; private init; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

}