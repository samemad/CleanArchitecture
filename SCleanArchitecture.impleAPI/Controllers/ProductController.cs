using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;

namespace SCleanArchitecture.SimpleAPI.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductRequestDto productRequestDto)
        {
            var result = await _productService.AddProduct(productRequestDto);
            return Ok(result);
        }

        // Get All Products endpoint
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return Ok(result);
        }

        [HttpGet("{id}")]  // {id} means it expects a parameter in the URL
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductById(id);

            // If product not found, return 404 Not Found
            if (result == null)
                return NotFound($"Product with ID {id} not found");

            // If found, return 200 OK with the product
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequestDto requestDto)
        {
            var result = await _productService.UpdateProduct(requestDto);

            // If product not found, return 404
            if (result == null)
                return NotFound($"Product with ID {requestDto.Id} not found");

            // If updated successfully, return 200 OK with updated product
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);

            if (!result)
                return NotFound($"Product with ID {id} not found");

            return Ok($"Product with ID {id} deleted successfully");
        }
    }
}