using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overview_WEBAPI.Data;
using Overview_WEBAPI.DTOs;
using Overview_WEBAPI.Models;

namespace Overview_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly MovieDbContext _context;

        //Aus dem IOC Container bekommen wir unseren MovieDBContext
        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
          if (_context.Movies == null) //wenn Movie nicht da hat
          {
              return NotFound(); //404 wird zurück gegeben
          }
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }


            Movie movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            //Sicherheitsprüfung -> beide Ids müssen gleich sein
            if (id != movie.Id)
            {
                return BadRequest();
            }

            //EfCore -> Sagen EF Core, dass movie ein modifizierter Status 
            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
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
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
          if (_context.Movies == null)
          {
              return Problem("Entity set 'MovieDbContext.Movie'  is null.");
          }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        [HttpPost ("POSTMultipleMovies")]
        public async Task<ActionResult> PostMovies(MoviesDTO moviesDTO)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'MovieDbContext.Movie'  is null.");
            }

            _context.Movies.AddRange(moviesDTO.Movies.ToArray());
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("POSTMultipleMoviesB")]
        public async Task<ActionResult> PostMovies(List<Movie> movies)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'MovieDbContext.Movie'  is null.");
            }

            _context.Movies.AddRange(movies.ToArray());
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
