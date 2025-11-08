using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;

namespace SCleanArchitecture.SimpleAPI.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderRequestDto orderRequestDto)
        {
            var result = await _orderService.AddOrder(orderRequestDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderById(id);

            if (result == null)
                return NotFound($"Order with ID {id} not found");

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderRequestDto requestDto)
        {
            var result = await _orderService.UpdateOrderStatus(requestDto);

            if (result == null)
                return NotFound($"Order with ID {requestDto.Id} not found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);

            if (!result)
                return NotFound($"Order with ID {id} not found");

            return Ok($"Order with ID {id} deleted successfully");
        }

        [HttpGet("customer/{email}")]
        public async Task<IActionResult> GetOrdersByCustomerEmail(string email)
        {
            var result = await _orderService.GetOrdersByCustomerEmail(email);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(string status)
        {
            var result = await _orderService.GetOrdersByStatus(status);
            return Ok(result);
        }
    }
}