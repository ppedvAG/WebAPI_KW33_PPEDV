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
    public class ErsteHilfeKursController : ControllerBase
    {
        private readonly UnfallPortalDbContext _context;

        public ErsteHilfeKursController(UnfallPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/ErsteHilfeKurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErsteHilfeKurs>>> GetErsteHilfeKurs()
        {
          if (_context.ErsteHilfeKurs == null)
          {
              return NotFound();
          }
            return await _context.ErsteHilfeKurs.ToListAsync();
        }

        // GET: api/ErsteHilfeKurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErsteHilfeKurs>> GetErsteHilfeKurs(int id)
        {
          if (_context.ErsteHilfeKurs == null)
          {
              return NotFound();
          }
            var ersteHilfeKurs = await _context.ErsteHilfeKurs.FindAsync(id);

            if (ersteHilfeKurs == null)
            {
                return NotFound();
            }

            return ersteHilfeKurs;
        }

        // PUT: api/ErsteHilfeKurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErsteHilfeKurs(int id, ErsteHilfeKurs ersteHilfeKurs)
        {
            if (id != ersteHilfeKurs.Id)
            {
                return BadRequest();
            }

            _context.Entry(ersteHilfeKurs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErsteHilfeKursExists(id))
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

        // POST: api/ErsteHilfeKurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ErsteHilfeKurs>> PostErsteHilfeKurs(ErsteHilfeKurs ersteHilfeKurs)
        {
          if (_context.ErsteHilfeKurs == null)
          {
              return Problem("Entity set 'UnfallPortalDbContext.ErsteHilfeKurs'  is null.");
          }
            _context.ErsteHilfeKurs.Add(ersteHilfeKurs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetErsteHilfeKurs", new { id = ersteHilfeKurs.Id }, ersteHilfeKurs);
        }

        // DELETE: api/ErsteHilfeKurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErsteHilfeKurs(int id)
        {
            if (_context.ErsteHilfeKurs == null)
            {
                return NotFound();
            }
            var ersteHilfeKurs = await _context.ErsteHilfeKurs.FindAsync(id);
            if (ersteHilfeKurs == null)
            {
                return NotFound();
            }

            _context.ErsteHilfeKurs.Remove(ersteHilfeKurs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErsteHilfeKursExists(int id)
        {
            return (_context.ErsteHilfeKurs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
