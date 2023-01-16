using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Category;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceCategory serviceCategory;

        public CategoryController(IServiceCategory serviceCategory)
        {
            this.serviceCategory = serviceCategory;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            var categoriesDto = await serviceCategory.GetAllCategoriesAsync();
            
            if (!categoriesDto.Any())
            {
                return NoContent();
            }

            return Ok(categoriesDto.OrderBy(a => a.Id));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDto = await serviceCategory.FindCategoryByIdAsync(id);
          
            if (categoryDto is null)
            {
                return NoContent();
            }

            return Ok(categoryDto);
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<ActionResult> Post([FromBody]CategoryCreationDTO categoryDto)
        {
            var createdCategory = await serviceCategory.AddCategoryAsync(categoryDto);

            if (createdCategory is null)
            {
                return BadRequest("Category object is invalid");
            }

            return Ok(createdCategory);
        }
    }
}
