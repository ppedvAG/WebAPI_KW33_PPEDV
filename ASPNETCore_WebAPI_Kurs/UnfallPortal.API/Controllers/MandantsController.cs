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
    public class MandantsController : ControllerBase
    {
        private readonly UnfallPortalDbContext _context;

        public MandantsController(UnfallPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/Mandants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mandant>>> GetMandant()
        {
            if (_context.Mandant == null)
            {
                return NotFound();
            }

            List<Mandant> mandants = await _context.Mandant.ToListAsync();
            return mandants;
        }

        // GET: api/Mandants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mandant>> GetMandant(int id)
        {
          if (_context.Mandant == null)
          {
              return NotFound();
          }
            var mandant = await _context.Mandant.FindAsync(id);

            if (mandant == null)
            {
                return NotFound();
            }

            return mandant;
        }

        // PUT: api/Mandants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMandant(int id, Mandant mandant)
        {
            if (id != mandant.Id)
            {
                return BadRequest();
            }

            _context.Entry(mandant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MandantExists(id))
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

        // POST: api/Mandants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/NeuerMandant")]
        public async Task<ActionResult<Mandant>> PostMandant(Mandant mandant)
        {
          if (_context.Mandant == null)
          {
              return Problem("Entity set 'UnfallPortalDbContext.Mandant'  is null.");
          }
            _context.Mandant.Add(mandant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMandant", new { id = mandant.Id }, mandant);
        }

        // DELETE: api/Mandants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMandant(int id)
        {
            if (_context.Mandant == null)
            {
                return NotFound();
            }
            var mandant = await _context.Mandant.FindAsync(id);
            if (mandant == null)
            {
                return NotFound();
            }

            _context.Mandant.Remove(mandant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MandantExists(int id)
        {
            return (_context.Mandant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
