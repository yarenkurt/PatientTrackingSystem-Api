using System.Threading.Tasks;
using Business.Repositories;
using Core.Signatures;
using Microsoft.AspNetCore.Mvc;

namespace Api.Repositories
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerRepository<TService,TEntity> : ControllerBase
        where TEntity : class, IBaseEntity, new()
        where TService : IServiceRepository<TEntity>
    {

        private TService _service;
        
        public ControllerRepository(TService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _service.GetAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            return Ok(await _service.InsertAsync(entity));
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id,[FromBody] TEntity entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
    }
}