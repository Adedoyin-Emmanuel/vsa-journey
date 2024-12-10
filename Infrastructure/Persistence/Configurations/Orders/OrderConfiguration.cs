using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vsa_journey.Domain.Entities.Order;



namespace vsa_journey.Infrastructure.Persistence.Configurations.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);

        builder.HasOne(order => order.User)
            .WithOne()
            .HasForeignKey<Order>(order => order.UserId);

        builder.Property(order => order.Status).HasDefaultValue(OrderStatus.Pending);

        builder.Property(order => order.TotalAmount).IsRequired();
        
        builder.Property(order => order.PaymentStatus).HasDefaultValue(PaymentStatus.Pending);
        
        builder.Property(order => order.CreatedAt).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Property(order => order.UpdatedAt).IsRequired().ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}