using FilmClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FilmClient.Model.DTOs;

namespace FilmClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FilmService filmService;

        public IndexModel(FilmService filmService)
        {
            this.filmService = filmService;
        }

        public List<MovieDTO> Movies { get; set; } = new();
        public List<DirectorDTO> Directors { get; set; } = new();

        public async Task OnGetAsync()
        {
            Movies = await filmService.GetMovies() ;
            Directors = await filmService.GetDirectors();
        }

        

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {

            var response = await filmService.DeleteMovieAsync(id);

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("/CreateNewDirector");
        }
    }
}
