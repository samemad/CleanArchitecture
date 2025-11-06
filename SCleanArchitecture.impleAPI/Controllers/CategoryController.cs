using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;

namespace SCleanArchitecture.SimpleAPI.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequestDto categoryRequestDto)
        {
            var result = await _categoryService.AddCategory(categoryRequestDto);
            return Ok(result);
        }

        // Get All Category endpoint
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategories();
            return Ok(result);
        }

        [HttpGet("{id}")]  // {id} means it expects a parameter in the URL
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _categoryService.GetCategoryById(id);

            // If Category not found, return 404 Not Found
            if (result == null)
                return NotFound($"Category with ID {id} not found");

            // If found, return 200 OK with the Category
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryRequestDto requestDto)
        {
            var result = await _categoryService.UpdateCategory(requestDto);

            // If Category not found, return 404
            if (result == null)
                return NotFound($"Category with ID {requestDto.Id} not found");

            // If updated successfully, return 200 OK with updated Category
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);

            if (!result)
                return NotFound($"Category with ID {id} not found");

            return Ok($"Category with ID {id} deleted successfully");
        }
    }
}