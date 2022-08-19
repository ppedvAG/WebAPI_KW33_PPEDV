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
    public class MandantKursController : ControllerBase
    {
        private readonly UnfallPortalDbContext _context;

        public MandantKursController(UnfallPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/MandantKurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MandantKurs>>> GetMandantKurs()
        {
          if (_context.MandantKurs == null)
          {
              return NotFound();
          }
            return await _context.MandantKurs.ToListAsync();
        }

        // GET: api/MandantKurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MandantKurs>> GetMandantKurs(int id)
        {
          if (_context.MandantKurs == null)
          {
              return NotFound();
          }
            var mandantKurs = await _context.MandantKurs.FindAsync(id);

            if (mandantKurs == null)
            {
                return NotFound();
            }

            return mandantKurs;
        }

        // PUT: api/MandantKurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMandantKurs(int id, MandantKurs mandantKurs)
        {
            if (id != mandantKurs.Id)
            {
                return BadRequest();
            }

            _context.Entry(mandantKurs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MandantKursExists(id))
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

        // POST: api/MandantKurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MandantKurs>> PostMandantKurs(MandantKurs mandantKurs)
        {
          if (_context.MandantKurs == null)
          {
              return Problem("Entity set 'UnfallPortalDbContext.MandantKurs'  is null.");
          }
            _context.MandantKurs.Add(mandantKurs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMandantKurs", new { id = mandantKurs.Id }, mandantKurs);
        }

        // DELETE: api/MandantKurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMandantKurs(int id)
        {
            if (_context.MandantKurs == null)
            {
                return NotFound();
            }
            var mandantKurs = await _context.MandantKurs.FindAsync(id);
            if (mandantKurs == null)
            {
                return NotFound();
            }

            _context.MandantKurs.Remove(mandantKurs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MandantKursExists(int id)
        {
            return (_context.MandantKurs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
