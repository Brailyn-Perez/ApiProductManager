using Microsoft.EntityFrameworkCore;
using ProductManager.API.Entities.User;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> users { get; set; }
}
