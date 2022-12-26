using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models.Base;
using Microsoft.AspNetCore.Mvc;


namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TRepositoryBase> : ControllerBase
        where TEntity : EntityBase
        where TRepositoryBase : IRepositoryBase<TEntity>
    {
        private readonly TRepositoryBase repository;
        public BaseController(TRepositoryBase repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAsync()
        {
            return await repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetAsync(int id)
        {
            var entity = await repository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> PutAsync(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            await repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> PostAsync(TEntity entity)
        {
            await repository.AddAsync(entity);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> DeleteAsync(int id)
        {
            var entity = await repository.DeleteAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }
    }
}

