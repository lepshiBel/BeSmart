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
        private readonly ILogger<AnswersController> logger;

        public CategoryController(IServiceCategory serviceCategory, ILogger<AnswersController> logger)
        {
            this.serviceCategory = serviceCategory;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            var categoriesDto = await serviceCategory.GetAllCategoriesAsync();
            
            if (!categoriesDto.Any())
            {
                logger.LogError("Something went wrong in method GetAll category");
                return NoContent();
            }

            logger.LogInformation("Method GetAll in category worked");
            return Ok(categoriesDto.OrderBy(a => a.Id));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDto = await serviceCategory.FindCategoryByIdAsync(id);
          
            if (categoryDto is null)
            {
                logger.LogError("Something went wrong in method Get category");
                return NoContent();
            }

            logger.LogInformation("Method Get in category worked");
            return Ok(categoryDto);
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<ActionResult> Post([FromBody]CategoryCreationDTO categoryDto)
        {
            var createdCategory = await serviceCategory.AddCategoryAsync(categoryDto);

            if (createdCategory is null)
            {
                logger.LogError("Something went wrong in method Create category");
                return BadRequest("Category object is invalid");
            }

            logger.LogInformation("Method post in category worked");
            return Ok(createdCategory);
        }
    }
}
