using Cheeseria.API.Data;
using Cheeseria.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace Cheeseria.API.Controllers
{
    [Route("api/cheese")]
    [ApiController]
    public class CheeseController : ControllerBase
    {
        private readonly CheeseContext _cheeseContext;

        public CheeseController(CheeseContext context)
        {
            _cheeseContext = context;
        }

        // GET: api/<CheeseController>
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(IEnumerable<Cheese>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Cheese>>> GetAsync()
        {
            // TODO: add paging
            var cheeseItems = await _cheeseContext.Cheeses.ToListAsync();
            return cheeseItems;
        }

        // GET api/<CheeseController>/5
        [HttpGet]
        [Route("items/{cheeseId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Cheese), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cheese>> GetByIdAsync(int cheeseId)
        {
            if (cheeseId < 0) { return BadRequest(); }

            var item = await _cheeseContext.Cheeses.SingleOrDefaultAsync(c => c.Id == cheeseId);

            if (item == null) { return NotFound(); }

            return item;
        }

        // POST api/<CheeseController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CheeseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CheeseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
