using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vsa_journey.Domain.Entities.Token;
using vsa_journey.Domain.Entities.User;

namespace vsa_journey.Infrastructure.Persistence.Configurations.Tokens;

public class TokenConfiguration : IEntityTypeConfiguration<Token>    
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(token => token.Value).IsRequired().HasMaxLength(200);

        builder.Property(token => token.IsRevoked).IsRequired();
        
        builder.Property(token => token.UserId).IsRequired();
        
        builder.Property(token => token.Type).IsRequired();
        
        builder.Property(token => token.ExpiresAt).IsRequired();
        
        
        builder.HasOne(token => token.User).WithMany().HasForeignKey(token => token.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(product => product.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Property(product => product.UpdatedAt)
            .IsRequired()
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}