using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FormattersSampleWebAPI.Data;
using FormattersSampleWebAPI.Models;

namespace FormattersSampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballClubsController : ControllerBase
    {
        private readonly FootballClubDb _context;

        public FootballClubsController(FootballClubDb context)
        {
            _context = context;
        }

        // GET: api/FootballClubs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FootballClub>>> GetFootballClub()
        {
          if (_context.FootballClub == null)
          {
              return NotFound();
          }
            return await _context.FootballClub.ToListAsync();
        }

        // GET: api/FootballClubs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FootballClub>> GetFootballClub(int id)
        {
          if (_context.FootballClub == null)
          {
              return NotFound();
          }
            var footballClub = await _context.FootballClub.FindAsync(id);

            if (footballClub == null)
            {
                return NotFound();
            }

            return footballClub;
        }

        // PUT: api/FootballClubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFootballClub(int id, FootballClub footballClub)
        {
            if (id != footballClub.Id)
            {
                return BadRequest();
            }

            _context.Entry(footballClub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FootballClubExists(id))
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

        // POST: api/FootballClubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FootballClub>> PostFootballClub(FootballClub footballClub)
        {
          if (_context.FootballClub == null)
          {
              return Problem("Entity set 'FootballClubDb.FootballClub'  is null.");
          }
            _context.FootballClub.Add(footballClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFootballClub", new { id = footballClub.Id }, footballClub);
        }

        // DELETE: api/FootballClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFootballClub(int id)
        {
            if (_context.FootballClub == null)
            {
                return NotFound();
            }
            var footballClub = await _context.FootballClub.FindAsync(id);
            if (footballClub == null)
            {
                return NotFound();
            }

            _context.FootballClub.Remove(footballClub);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FootballClubExists(int id)
        {
            return (_context.FootballClub?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
