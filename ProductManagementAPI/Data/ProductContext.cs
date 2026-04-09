using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Data;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options)
        : base(options)
    {
        
    }

    // cria um filtro global para não trazer os registros onde o deletedAt é nulo
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
        .HasQueryFilter(p => p.DeletedAt == null);
    }

    public DbSet<Product> Products { get; set; }

}
