using FilmClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FilmClient.Model.DTOs;

namespace FilmClient.Pages
{
    public class MovieEditModel : PageModel
    {
        private readonly FilmService filmService;

        public MovieEditModel(FilmService filmService)
        {
            this.filmService = filmService;
        }

        [BindProperty]
        public int id { get; set; }

        [BindProperty]
        public MovieDTO movieDTO { get; set; }

        public async Task OnGetAsync()
        {

        }

        public async Task OnPostAsync()
        {
            movieDTO.DirectorIds = new();
            movieDTO.MovieId = id;
            await filmService.UpdateMovieAsync(movieDTO);
        }
    }
}

