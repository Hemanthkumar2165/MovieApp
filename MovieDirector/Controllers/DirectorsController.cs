using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDirector.Context;
using MovieDirector.DTO;
using MovieDirector.Model;

namespace MovieDirector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly DataContext _context;

        public DirectorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DirectorDTO>>> Getdirectors()
        {
            var ddd = await
                    _context.directors.Select(dirObj => new DirectorDTO
                    {
                        DirectorId = dirObj.DirectorId,
                        Name = dirObj.Name,
                        IsActive = dirObj.IsActive,
                        movies = dirObj.Movies
                    .Select(m => new MovieDTO
                    {
                        Title = m.Title,
                        MainLanguage = m.MainLanguage,
                        MovieId = m.MovieId,
                        Rating = m.Rating,
                        YearofRelease = m.YearofRelease
                    }).ToList()
                    })
                    .ToListAsync();
            return ddd;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<DirectorDTO>>> GetDirector(int id)
        {
            var director = await _context.directors.Include(d => d.Movies
            ).Where(d => d.DirectorId == id).
                Select(x => new DirectorDTO
                {
                    Name = x.Name,
                    movies = x.Movies.Select(m => new MovieDTO
                    {
                        Title = m.Title,
                        MainLanguage = m.MainLanguage,
                        MovieId = m.MovieId,
                        Rating = m.Rating,
                        YearofRelease = m.YearofRelease
                    }).ToList()
                }).ToListAsync();

            if (director == null)
            {
                return NotFound();
            }

            return director;
        }

        // PUT: api/Directors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirector(int id, Director director)
        {
            if (id != director.DirectorId)
            {
                return BadRequest();
            }

            _context.Entry(director).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
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

        // POST: api/Directors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Director>> PostDirector(DirectorDTO director)
        {

            var Director = new Director()
            {
                Name = director.Name,
                IsActive = director.IsActive
            };
            _context.directors.Add(Director);
            await _context.SaveChangesAsync();

            return Director;
        }

        // DELETE: api/Directors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var director = await _context.directors.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }

            _context.directors.Remove(director);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DirectorExists(int id)
        {
            return _context.directors.Any(e => e.DirectorId == id);
        }
    }
}
