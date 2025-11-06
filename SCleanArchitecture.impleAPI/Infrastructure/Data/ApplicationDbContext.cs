// Here is your DATABASE CONNECTION file!! 
// It tells Entity Framework about your tables (DbSets)

using Microsoft.EntityFrameworkCore;
using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    // Constructor - receives configuration from Program.cs
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet = Database Table
    // Each DbSet becomes a table in your database
    public DbSet<User> Users { get; set; }  // Creates "Users" table
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    // When you add more entities, add them here:

    // public DbSet<Order> Orders { get; set; }

    // This method configures your database tables
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User table
        modelBuilder.Entity<User>(entity =>
        {
            // Set primary key
            entity.HasKey(u => u.Id); // this means like take u and give me u.Name !

            // Configure properties
            entity.Property(u => u.Name)
                .IsRequired()           // NOT NULL and make it required
                .HasMaxLength(100);     // VARCHAR(100)

            entity.Property(u => u.Email)
                .IsRequired()           // NOT NULL
                .HasMaxLength(255);     // VARCHAR(255)

            entity.Property(u => u.CreatedAt)
                .IsRequired();          // NOT NULL

            // Add unique constraint on Email (optional but recommended)
            entity.HasIndex(u => u.Email)
                .IsUnique();
        });

        //  Configure Category table 
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);  // Primary Key

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Description)
                .HasMaxLength(500);

            entity.Property(c => c.CreatedAt)
                .IsRequired();
        });

        // Configure Product table 
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);  // Primary Key

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Description)
                .HasMaxLength(1000);

            entity.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");  // Price with 2 decimal places

            entity.Property(p => p.Stock)
                .IsRequired();

            entity.Property(p => p.CreatedAt)
                .IsRequired();

            //  Define the relationship: Product belongs to Category
            entity.HasOne(p => p.Category)        // One Product has...
                  .WithMany(c => c.Products)      // ...One Category which has many Products
                  .HasForeignKey(p => p.CategoryId);  // Using CategoryId as Foreign Key
        });

    }
}