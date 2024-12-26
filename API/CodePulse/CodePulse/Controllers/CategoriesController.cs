
using Domain.Models;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace CodePulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryRequestDto request)
        {
            try
            {
                _categoryService.CreateCategory(request);
                return StatusCode(StatusCodes.Status201Created, "Category created");
            }
            catch (ArgumentException ex) // For invalid arguments
            {
                return BadRequest($"Invalid input: {ex.Message}");
            }
            catch (InvalidOperationException ex) // For invalid operations
            {
                return BadRequest($"Operation error: {ex.Message}");
            }
            catch (Exception ex) // General catch-all for unexpected errors
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Unexpected error: {ex.Message}");
            }

        }

        [HttpGet]
        public ActionResult<List<CategoryDto>> GetAll()
        {
            try
            {
                var categories = _categoryService.GetAllCategories();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDto> GetById([FromRoute] Guid id)
        {
            try
            {
                var category = _categoryService.GetCategoryById(id);
                return Ok(category);
            }
            catch (ArgumentException ex) // For invalid arguments (empty ID)
            {
                return BadRequest($"Invalid input: {ex.Message}");
            }
            catch (KeyNotFoundException ex) // If category is not found
            {
                return NotFound($"Category not found: {ex.Message}");
            }
            catch (Exception ex) // General catch-all for unexpected errors
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Unexpected error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            try
            {
                _categoryService.UpdateCategoryRequestDto(request, id);
                return StatusCode(StatusCodes.Status200OK, "Category updated");
            }
            catch (ArgumentException ex) // For invalid arguments
            {
                return BadRequest($"Invalid input: {ex.Message}");
            }
            catch (InvalidOperationException ex) // For invalid operations
            {
                return BadRequest($"Operation error: {ex.Message}");
            }
            catch (Exception ex) // General catch-all for unexpected errors
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Unexpected error: {ex.Message}");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromRoute] Guid id)
        {
            try
            {
                // Call the service to delete the category
                _categoryService.DeleteCategory(id);

                // Return a successful response
                return StatusCode(StatusCodes.Status200OK, "Category deleted");
            }
            catch (ArgumentException ex) // For invalid arguments (empty ID)
            {
                return BadRequest($"Invalid input: {ex.Message}");
            }
            catch (KeyNotFoundException ex) // If category is not found
            {
                return NotFound($"Category not found: {ex.Message}");
            }
            catch (Exception ex) // General catch-all for unexpected errors
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Unexpected error: {ex.Message}");
            }
        }
    }
}
