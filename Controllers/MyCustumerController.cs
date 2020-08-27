using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAspCore.Models;

namespace ApiAspCore.Controllers
{
    [Route("api/custumers")]
    [ApiController]
    public class MyCustumerController : ControllerBase
    {
        private readonly MyApiContext _context;

        public MyCustumerController(MyApiContext context)
        {
            _context = context;
        }

        // GET: api/MyCustumer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Custumer>>> GetCustumers()
        {
            return await _context.Custumers.ToListAsync();
        }

        // GET: api/MyCustumer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Custumer>> GetCustumer(int id)
        {
            var custumer = await _context.Custumers.FindAsync(id);

            if (custumer == null)
            {
                return NotFound();
            }

            return custumer;
        }

        // PUT: api/MyCustumer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustumer(int id, Custumer custumer)
        {
            if (id != custumer.Id)
            {
                return BadRequest();
            }

            _context.Entry(custumer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustumerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MyCustumer
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Custumer>> PostCustumer(Custumer custumer)
        {
            _context.Custumers.Add(custumer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustumer", new { id = custumer.Id }, custumer);
        }

        // DELETE: api/MyCustumer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Custumer>> DeleteCustumer(int id)
        {
            var custumer = await _context.Custumers.FindAsync(id);
            if (custumer == null)
            {
                return NotFound();
            }

            _context.Custumers.Remove(custumer);
            await _context.SaveChangesAsync();

            return custumer;
        }

        private bool CustumerExists(int id)
        {
            return _context.Custumers.Any(e => e.Id == id);
        }
    }
}
