using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Category> Category { get; set; }
    public DbSet<FoodType> FoodType { get; set; }
    public DbSet<MenuItem> MenuItem { get; set; }
    
    public DbSet<AppUser> ApplicationUser { get; set; }
    
    public DbSet<ShoppingCart> ShoppingCart { get; set; }

    public DbSet<OrderHeader> OrderHeader { get; set; }
    
    public DbSet<OrderDetails> OrderDetails { get; set; }
}