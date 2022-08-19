using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnfallPortal.API.Data;
using UnfallPortal.Shared.Entities;

namespace UnfallPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MandantUnfallsController : ControllerBase
    {
        private readonly UnfallPortalDbContext _context;

        public MandantUnfallsController(UnfallPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/MandantUnfalls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MandantUnfall>>> GetMandantUnfall()
        {
          if (_context.MandantUnfall == null)
          {
              return NotFound();
          }
            // var tempContext = _context.MandantUnfall.Include(m => m.MandantRef);
            return await _context.MandantUnfall.ToListAsync();
        }

        // GET: api/MandantUnfalls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MandantUnfall>> GetMandantUnfall(int id)
        {
          if (_context.MandantUnfall == null)
          {
              return NotFound();
          }
            var mandantUnfall = await _context.MandantUnfall.FindAsync(id);

            if (mandantUnfall == null)
            {
                return NotFound();
            }

            return mandantUnfall;
        }

        // PUT: api/MandantUnfalls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMandantUnfall(int id, MandantUnfall mandantUnfall)
        {
            if (id != mandantUnfall.Id)
            {
                return BadRequest();
            }

            _context.Entry(mandantUnfall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MandantUnfallExists(id))
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

        // POST: api/MandantUnfalls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MandantUnfall>> PostMandantUnfall(MandantUnfall mandantUnfall)
        {
          if (_context.MandantUnfall == null)
          {
              return Problem("Entity set 'UnfallPortalDbContext.MandantUnfall'  is null.");
          }
            _context.MandantUnfall.Add(mandantUnfall);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMandantUnfall", new { id = mandantUnfall.Id }, mandantUnfall);
        }

        // DELETE: api/MandantUnfalls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMandantUnfall(int id)
        {
            if (_context.MandantUnfall == null)
            {
                return NotFound();
            }
            var mandantUnfall = await _context.MandantUnfall.FindAsync(id);
            if (mandantUnfall == null)
            {
                return NotFound();
            }

            _context.MandantUnfall.Remove(mandantUnfall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MandantUnfallExists(int id)
        {
            return (_context.MandantUnfall?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
