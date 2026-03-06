using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace  dotnet_store.Models;

public class DataContext : DbContext
{
    // constructor methods
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
        
    }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new List<Product>()
            {
                new Product(){ Id = 1, ProductName="Apple Watch 6",Price=30000, IsActive=true},
                new Product(){ Id = 2, ProductName="Apple Watch 7",Price=35000, IsActive=false},
                new Product(){ Id = 3, ProductName="Apple Watch 8",Price=40000, IsActive=true},
                new Product(){ Id = 4, ProductName="Apple Watch 9",Price=45000, IsActive=true},
                new Product(){ Id = 5, ProductName="Apple Watch 10",Price=50000, IsActive=false},
                new Product(){ Id = 6, ProductName="Apple Watch 11",Price=55000, IsActive=true}
            }
        );
    }
}
 