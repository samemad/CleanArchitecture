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

    // When you add more entities, add them here:
    // public DbSet<Product> Products { get; set; }
    // public DbSet<Category> Categories { get; set; }
    // public DbSet<Order> Orders { get; set; }

    // This method configures your database tables
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User table
        modelBuilder.Entity<User>(entity =>
        {
            // Set primary key
            entity.HasKey(u => u.Id);

            // Configure properties
            entity.Property(u => u.Name)
                .IsRequired()           // NOT NULL
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
    }
}