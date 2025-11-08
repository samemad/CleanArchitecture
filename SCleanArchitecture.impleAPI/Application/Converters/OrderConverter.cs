using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using System.Linq;

namespace SCleanArchitecture.SimpleAPI.Application.Converters;

internal static class OrderConverter
{
    // Convert AddOrderRequestDto → Order Entity
    public static Order ToOrderEntity(this AddOrderRequestDto requestDto)
    {
        return new Order
        {
            CustomerName = requestDto.CustomerName,
            CustomerEmail = requestDto.CustomerEmail,
            Status = "Pending"
        };
    }

    // Convert Order Entity → AddOrderResponseDto
    public static AddOrderResponseDto ToAddOrderResponse(this Order order)
    {
        return new AddOrderResponseDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            CustomerEmail = order.CustomerEmail,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            Items = order.OrderItems?.Select(oi => new OrderItemResponseDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                Subtotal = oi.Subtotal
            }).ToList() ?? new List<OrderItemResponseDto>()
        };
    }

    // Convert UpdateOrderRequestDto → Order Entity
    public static Order ToOrderEntity(this UpdateOrderRequestDto requestDto)
    {
        return new Order
        {
            Id = requestDto.Id,
            Status = requestDto.Status
        };
    }

    // Helper: Convert OrderItemRequestDto to OrderItem Entity
    public static OrderItem ToOrderItemEntity(this OrderItemRequestDto dto, decimal unitPrice)
    {
        return new OrderItem
        {
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            UnitPrice = unitPrice,
            Subtotal = dto.Quantity * unitPrice
        };
    }
}