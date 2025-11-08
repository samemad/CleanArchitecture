using SCleanArchitecture.SimpleAPI.Application.Converters;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;
using System.Linq;

namespace SCleanArchitecture.SimpleAPI.Application.Services;

public interface IOrderService
{
    Task<AddOrderResponseDto> AddOrder(AddOrderRequestDto requestDto);
    Task<List<AddOrderResponseDto>> GetAllOrders();
    Task<AddOrderResponseDto> GetOrderById(int id);
    Task<AddOrderResponseDto> UpdateOrderStatus(UpdateOrderRequestDto requestDto);
    Task<bool> DeleteOrder(int id);
    Task<List<AddOrderResponseDto>> GetOrdersByCustomerEmail(string email);
    Task<List<AddOrderResponseDto>> GetOrdersByStatus(string status);
}

internal sealed class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<AddOrderResponseDto> AddOrder(AddOrderRequestDto requestDto)
    {
        // Validate request
        if (!requestDto.IsValid())
        {
            return OrderErrors.InvalidRequest();
        }

        // Create order entity
        var orderEntity = requestDto.ToOrderEntity();
        orderEntity.OrderItems = new List<OrderItem>();

        decimal totalAmount = 0;

        // Process each item in the order
        foreach (var itemDto in requestDto.Items)
        {
            // Get product to validate it exists and get current price
            var product = await _productRepository.GetProductByIdAsync(itemDto.ProductId);

            if (product == null)
            {
                return OrderErrors.ProductNotFound(itemDto.ProductId);
            }

            // Check stock availability
            if (product.Stock < itemDto.Quantity)
            {
                return OrderErrors.InsufficientStock(product.Name, product.Stock);
            }

            // Create order item
            var orderItem = itemDto.ToOrderItemEntity(product.Price);
            orderEntity.OrderItems.Add(orderItem);

            // Calculate total
            totalAmount += orderItem.Subtotal;

            // Update product stock
            product.Stock -= itemDto.Quantity;
            await _productRepository.UpdateProductAsync(product);
        }

        orderEntity.TotalAmount = totalAmount;

        // Save order to database
        await _orderRepository.AddOrderAsync(orderEntity);

        // Get the saved order with all items
        var savedOrder = await _orderRepository.GetOrderWithItemsAsync(orderEntity.Id);

        // Convert to response
        return savedOrder.ToAddOrderResponse();
    }

    public async Task<List<AddOrderResponseDto>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrdersWithItemsAsync();
        return orders.Select(o => o.ToAddOrderResponse()).ToList();
    }

    public async Task<AddOrderResponseDto> GetOrderById(int id)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(id);

        if (order == null)
            return null;

        return order.ToAddOrderResponse();
    }

    public async Task<AddOrderResponseDto> UpdateOrderStatus(UpdateOrderRequestDto requestDto)
    {
        // Validate request
        if (!requestDto.IsValid())
        {
            return OrderErrors.InvalidRequest();
        }

        // Check if order exists
        var existingOrder = await _orderRepository.GetOrderByIdAsync(requestDto.Id);
        if (existingOrder == null)
            return null;

        // Update only the status
        existingOrder.Status = requestDto.Status;

        await _orderRepository.UpdateOrderAsync(existingOrder);

        // Get updated order with items
        var updatedOrder = await _orderRepository.GetOrderWithItemsAsync(requestDto.Id);
        return updatedOrder.ToAddOrderResponse();
    }

    public async Task<bool> DeleteOrder(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        if (order == null)
            return false;

        await _orderRepository.DeleteOrderAsync(id);
        return true;
    }

    public async Task<List<AddOrderResponseDto>> GetOrdersByCustomerEmail(string email)
    {
        var orders = await _orderRepository.GetOrdersByCustomerEmailAsync(email);
        return orders.Select(o => o.ToAddOrderResponse()).ToList();
    }

    public async Task<List<AddOrderResponseDto>> GetOrdersByStatus(string status)
    {
        var orders = await _orderRepository.GetOrdersByStatusAsync(status);
        return orders.Select(o => o.ToAddOrderResponse()).ToList();
    }
}

public static class OrderErrors
{
    public static AddOrderResponseDto InvalidRequest()
    {
        return new AddOrderResponseDto();
    }

    public static AddOrderResponseDto ProductNotFound(int productId)
    {
        return new AddOrderResponseDto();
    }

    public static AddOrderResponseDto InsufficientStock(string productName, int availableStock)
    {
        return new AddOrderResponseDto();
    }
}