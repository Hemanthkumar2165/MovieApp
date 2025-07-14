using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDirector.Context;
using MovieDirector.DTO;
using MovieDirector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MovieDirector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> Getmovies()
        {
            var x=await _context.movies.Include(m=>m.Directors).
                Select(mov=>new MovieDTO { Title = mov.Title, Rating = mov.Rating, MainLanguage = mov.MainLanguage ,MovieId = mov.MovieId, YearofRelease = mov.YearofRelease, DirectorIds = mov.Directors.Select(x => x.DirectorId).ToList()})            
                .ToListAsync();
            return x;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDTO movieDTO)
        {
            if (id != movieDTO.MovieId)
            {
                return BadRequest();
            }

            Movie movieFind = await _context.movies.FindAsync(id);

            if (movieFind == null) return BadRequest();

            Movie movie = new Movie();
            movie.MovieId = id;
            movie.Title = (movieDTO.Title != null) ? movieDTO.Title : movieFind.Title;
            movie.Rating = (movieDTO.Rating != 0) ? movieDTO.Rating : movieFind.Rating;
            movie.MainLanguage = (movieDTO.MainLanguage != null) ? movieDTO.MainLanguage : movieFind.MainLanguage;
            movie.YearofRelease = (movieDTO.YearofRelease != 0) ? movieDTO.YearofRelease : movieFind.YearofRelease;

            _context.movies.Remove(movieFind);

            _context.Entry(movie).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieDTO movie)
        {
            var directorIds = movie.DirectorIds.ToList();
            List<Director> asd = [];
            

            foreach(var z in directorIds)
            {
                asd.Add(_context.directors.FirstOrDefault(m => m.DirectorId == z));  
            }
            var movies = new Movie
            {
                Title = movie.Title,
                YearofRelease = movie.YearofRelease,
                Rating = movie.Rating,
                MainLanguage = movie.MainLanguage,
                Directors = asd 
            };
            _context.movies.Add(movies);
            await _context.SaveChangesAsync();

            Console.WriteLine(_context.movies);

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movies);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.movies.Any(e => e.MovieId == id);
        }
    }
}
