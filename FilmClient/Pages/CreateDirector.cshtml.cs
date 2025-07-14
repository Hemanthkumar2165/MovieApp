using FilmClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FilmClient.Model.DTOs;

namespace FilmClient.Pages
{
    public class CreateDirectorModel : PageModel
    {

        private readonly FilmService filmService;

        public CreateDirectorModel(FilmService filmService)
        {
            this.filmService = filmService;
        }

        [BindProperty]
        public DirectorDTO Director { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Director.movies = new List<MovieDTO>();

            var response = await filmService.CreateDirectorAsync(Director);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            return Page();
        }
    }
}
