namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public sealed class AddOrderRequestDto
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public List<OrderItemRequestDto> Items { get; set; }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(CustomerName))
            return false;

        if (string.IsNullOrEmpty(CustomerEmail))
            return false;

        if (Items == null || Items.Count == 0)
            return false;

        return true;
    }
}