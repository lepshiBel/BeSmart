using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs;
using BeSmart.Domain.Models;
using BeSmart.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceCategory serviceCategory;
        private readonly IMapper mapper;

        public CategoryController(IServiceCategory serviceCategory, IMapper mapper)
        {
            this.serviceCategory = serviceCategory;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            var categories = await serviceCategory.GetAllCategoriesAsync();
            
            if (!categories.Any())
            {
                return NoContent();
            }

            return Ok(categories.Select(c => mapper.Map<CategoryDTO>(c)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await serviceCategory.FindCategoryByIdAsync(id);
          
            if (category is null)
            {
                return NoContent();
            }

            var categoryDto = mapper.Map<CategoryDTO>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post(CategoryCreationDTO categorydto)
        {
            var categoryToAdd = mapper.Map<Category>(categorydto);
            var createdCategory = await serviceCategory.AddCategoryAsync(categoryToAdd);

            if (createdCategory is null)
            {
                return BadRequest("Category object is invalid");
            }

            return Ok(createdCategory);
        }
    }
}
