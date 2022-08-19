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
    public class MandantUnfallDokumentesController : ControllerBase
    {
        private readonly UnfallPortalDbContext _context;

        public MandantUnfallDokumentesController(UnfallPortalDbContext context)
        {
            _context = context;
        }

        // GET: api/MandantUnfallDokumentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MandantUnfallDokumente>>> GetMandantUnfallDokumente()
        {
          if (_context.MandantUnfallDokumente == null)
          {
              return NotFound();
          }
            return await _context.MandantUnfallDokumente.ToListAsync();
        }

        // GET: api/MandantUnfallDokumentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MandantUnfallDokumente>> GetMandantUnfallDokumente(int id)
        {
          if (_context.MandantUnfallDokumente == null)
          {
              return NotFound();
          }
            var mandantUnfallDokumente = await _context.MandantUnfallDokumente.FindAsync(id);

            if (mandantUnfallDokumente == null)
            {
                return NotFound();
            }

            return mandantUnfallDokumente;
        }

        // PUT: api/MandantUnfallDokumentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMandantUnfallDokumente(int id, MandantUnfallDokumente mandantUnfallDokumente)
        {
            if (id != mandantUnfallDokumente.Id)
            {
                return BadRequest();
            }

            _context.Entry(mandantUnfallDokumente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MandantUnfallDokumenteExists(id))
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

        // POST: api/MandantUnfallDokumentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MandantUnfallDokumente>> PostMandantUnfallDokumente(MandantUnfallDokumente mandantUnfallDokumente)
        {
          if (_context.MandantUnfallDokumente == null)
          {
              return Problem("Entity set 'UnfallPortalDbContext.MandantUnfallDokumente'  is null.");
          }
            _context.MandantUnfallDokumente.Add(mandantUnfallDokumente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMandantUnfallDokumente", new { id = mandantUnfallDokumente.Id }, mandantUnfallDokumente);
        }

        // DELETE: api/MandantUnfallDokumentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMandantUnfallDokumente(int id)
        {
            if (_context.MandantUnfallDokumente == null)
            {
                return NotFound();
            }
            var mandantUnfallDokumente = await _context.MandantUnfallDokumente.FindAsync(id);
            if (mandantUnfallDokumente == null)
            {
                return NotFound();
            }

            _context.MandantUnfallDokumente.Remove(mandantUnfallDokumente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MandantUnfallDokumenteExists(int id)
        {
            return (_context.MandantUnfallDokumente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
