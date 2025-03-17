using Microsoft.EntityFrameworkCore;
using ProductManager.DAL.Context.Seeds;
using ProductManager.DAL.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategorySeed());
        modelBuilder.ApplyConfiguration(new ProductSeed());
        modelBuilder.ApplyConfiguration(new SupplierSeed());
    }
}
