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
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            var categories = await serviceCategory.GetAllCategoriesAsync();
            
            if (!categories.Any())
            {
                return NoContent();
            }

            return Ok(categories.Select(c => mapper.Map<CategoryDTO>(c)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await serviceCategory.FindCategoryByIdAsync(id);
            
            if (category is null)
            {
                return NoContent();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            var createdCategory = await serviceCategory.AddCategoryAsync(category);

            if (createdCategory is null)
            {
                return BadRequest("Category object is invalid");
            }

            return Ok(createdCategory);
        }
    }
}
