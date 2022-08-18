using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KonventionenSamplesWebAPI.Data;
using KonventionenSamplesWebAPI.Models;

namespace KonventionenSamplesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))] //Default-Convention für eine komplette Controller - Klasse 
    public class MoviesController : ControllerBase
    {
        private readonly KonventionenSamplesWebAPIContext _context;

        public MoviesController(KonventionenSamplesWebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
          if (_context.Movie == null)
          {
              return NotFound();
          }
            return await _context.Movie.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]

        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status406NotAcceptable)]

        
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
          if (_context.Movie == null)
          {
              return NotFound();
          }
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        //ApiConventionMethod -> is eine Sammlung von ProducesResponseType
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// Beschreibung einer Methode
        /// </summary>
        /// <param name="movie">Beschreibung des Parameters</param>
        /// <returns>Rückgabewert</returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
          if (_context.Movie == null)
          {
              return Problem("Entity set 'KonventionenSamplesWebAPIContext.Movie'  is null.");
          }
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
