namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public class AddOrderResponseDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public List<OrderItemResponseDto> Items { get; set; }
    public DateTime CreatedAt { get; set; }
}