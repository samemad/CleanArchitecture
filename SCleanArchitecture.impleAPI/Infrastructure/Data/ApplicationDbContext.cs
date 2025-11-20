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
    public DbSet<Order> Orders { get; set; }         
    public DbSet<OrderItem> OrderItems { get; set; }  

    // When you add more entities, add them here:

 

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

            // Configure PasswordHash
            entity.Property(u => u.PasswordHash)
                .IsRequired()           // NOT NULL - every user must have a password
                .HasMaxLength(500);     // BCrypt hashes are ~60 chars, but give extra space


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
                  .HasForeignKey(p => p.CategoryId)  // Using CategoryId as Foreign Key
                  .OnDelete(DeleteBehavior.Restrict);  // Prevent deleting Category if Products exist   
        });

        // Order configuration - NEW
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.Property(o => o.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(o => o.CustomerEmail)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(o => o.Status)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(o => o.CreatedAt)
                .IsRequired();
        });

        // OrderItem configuration - NEW
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.Id);

            entity.Property(oi => oi.Quantity)
                .IsRequired();

            entity.Property(oi => oi.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(oi => oi.Subtotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Relationship: OrderItem belongs to Order
            entity.HasOne(oi => oi.Order)
                  .WithMany(o => o.OrderItems)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);  // Delete items when order is deleted

            // Relationship: OrderItem references Product
            entity.HasOne(oi => oi.Product)
                  .WithMany()
                  .HasForeignKey(oi => oi.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);  // Don't delete product when order item is deleted
        });


    }
}