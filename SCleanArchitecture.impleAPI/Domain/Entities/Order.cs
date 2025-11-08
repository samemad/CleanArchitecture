namespace SCleanArchitecture.SimpleAPI.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }  // "Pending", "Completed", "Cancelled"

    // Navigation Property - One Order has many OrderItems
    public ICollection<OrderItem> OrderItems { get; set; }

    public DateTime CreatedAt { get; set; }
}