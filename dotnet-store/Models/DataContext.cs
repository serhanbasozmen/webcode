using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace  dotnet_store.Models;

public class DataContext : IdentityDbContext<IdentityUser>
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
        
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Slider> Sliders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Slider>().HasData( 
            new List<Slider>
            {
                new Slider{ Id=1, Title="Slider 1 Title", Explanation="Slider 1 Explanataion" , Image="slider-1.jpeg", IsActive=true, Index=0},
                new Slider{ Id=2, Title="Slider 2 Title", Explanation="Slider 2 Explanataion" , Image="slider-2.jpeg", IsActive=true, Index=1},
                new Slider{ Id=3, Title="Slider 3 Title", Explanation="Slider 3 Explanataion" , Image="slider-3.jpeg", IsActive=true, Index=2},
            }
        );

        modelBuilder.Entity<Category>().HasData(
        new List<Category>
        {
            new Category{Id=1,CategoryId="Phone",Url="phone"},
            new Category{Id=2,CategoryId="Electronic",Url="electronic"},
            new Category{Id=3,CategoryId="Major Appliances",Url="major appliances"},
            new Category{Id=4,CategoryId="Clothes",Url="clothes"},
            new Category{Id=5,CategoryId="Costemic",Url="costemic"},
            new Category{Id=6,CategoryId="Category 1",Url="category-1"},
            new Category{Id=7,CategoryId="Category 2",Url="category-2"},
            new Category{Id=8,CategoryId="Category 3",Url="category-3"},
            new Category{Id=9,CategoryId="Category 4",Url="category-4"},
            new Category{Id=10,CategoryId="Category 5",Url="category-5"},
        }
      );

        modelBuilder.Entity<Product>().HasData(
            new List<Product>()
            {
                new Product(){ 
                    Id = 1, 
                    ProductName="Apple Watch 6",
                    Price=30000, 
                    IsActive=true, 
                    Image="1.jpeg", 
                    Homepage=true, 
                    Explanation=" Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.",
                    CategoryId=1
                    },

                new Product(){ 
                    Id = 2, 
                    ProductName="Apple Watch 7",
                    Price=35000, 
                    IsActive=false, 
                    Image="2.jpeg", 
                    Homepage=true, 
                    Explanation=" Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.",
                    CategoryId=2},

                new Product(){ 
                    Id = 3, 
                    ProductName="Apple Watch 8",
                    Price=40000, 
                    IsActive=true, 
                    Image="3.jpeg", 
                    Homepage=true, 
                    Explanation=" Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.",
                    CategoryId=3},

                new Product(){ 
                    Id = 4, 
                    ProductName="Apple Watch 9",
                    Price=45000, 
                    IsActive=true, 
                    Image="4.jpeg", 
                    Homepage=false, 
                    Explanation=" Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.",
                    CategoryId=4},

                new Product(){ 
                    Id = 5, 
                    ProductName="Apple Watch 10",
                    Price=50000, 
                    IsActive=false, 
                    Image="5.jpeg", 
                    Homepage=true, 
                    Explanation=" Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.",
                    CategoryId=5},

                new Product(){ 
                    Id = 6, 
                    ProductName="Apple Watch 11",
                    Price=55000, 
                    IsActive=true, 
                    Image="6.jpeg", 
                    Homepage=true, 
                    Explanation=" Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.",
                    CategoryId=2},

                new Product(){ 
                    Id = 7, 
                    ProductName="Apple Watch 12",
                    Price=60000, 
                    IsActive=true, 
                    Image="7.jpeg", 
                    Homepage=true, 
                    Explanation=" Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.",
                    CategoryId=3},
                    
                new Product(){ 
                    Id = 8, 
                    ProductName="Apple Watch 13",
                    Price=65000, 
                    IsActive=true, 
                    Image="8.jpeg", 
                    Homepage=true, 
                    Explanation=" Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.",
                    CategoryId=4}
            }
        );
    }
}
 
 