using FilmClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FilmClient.Model.DTOs;

namespace FilmClient.Pages
{
    public class CreateMovieModel : PageModel
    {
        private readonly FilmService filmService;

        public CreateMovieModel(FilmService filmService)
        {
            this.filmService = filmService;
        }

        [BindProperty]
        public MovieDTO Movie { get; set; } = new MovieDTO();

        [BindProperty]
        public List<int> SelectedDirectors { get; set; }

        public List<DirectorDTO> AllDirectors { get; set; }

        public async Task OnGetAsync()
        {
            AllDirectors = await filmService.GetDirectors();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            Movie.DirectorIds = SelectedDirectors;

            var response = await filmService.CreateMovieAsync(Movie);

            return RedirectToPage("Index");
        }

    }
}
