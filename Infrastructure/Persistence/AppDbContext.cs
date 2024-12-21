using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using vsa_journey.Domain.Entities.Category;
using vsa_journey.Domain.Entities.Order;
using vsa_journey.Domain.Entities.Product;
using vsa_journey.Domain.Entities.User;

namespace vsa_journey.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }    
    
    public DbSet<User> Users { get; set; }  
    public DbSet<Order> Orders { get; set; }    
    public DbSet<Product> Products { get; set; }    
    public DbSet<Category> Categories { get; set; }
    
}