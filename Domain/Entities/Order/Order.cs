namespace vsa_journey.Domain.Entities.Order;

public class Order
{
    public Guid Id { get; private init; }
    
    public Guid UserId { get; private init; }
    public User.User User { get; private init; }
    
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal TotalAmount { get; set; }
    
    public DateTime? CancelledAt { get; private init; }
    
    public string? CancellationReason { get; set; }
    
    public DateTime? ExpectedDeliveryDate { get; set; }
    
    public DateTime? ActualDeliveryDate { get; set; }

    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    
    public ICollection<Guid> ProductIds { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    

}