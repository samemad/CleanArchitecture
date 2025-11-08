namespace SCleanArchitecture.SimpleAPI.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }

    // Foreign Key to Order
    public int OrderId { get; set; }
    public Order Order { get; set; }  // Navigation Property

    // Foreign Key to Product
    public int ProductId { get; set; }
    public Product Product { get; set; }  // Navigation Property

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }  // Price at time of order
    public decimal Subtotal { get; set; }   // Quantity * UnitPrice
}