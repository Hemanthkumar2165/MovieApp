using FilmClient.Model;
using System.Net.Http;
using static FilmClient.Model.DTOs;

namespace FilmClient.Services
{
    public class FilmService
    {
        private readonly HttpClient httpClient;

        public FilmService(IHttpClientFactory httpClientFactory )
        {
            httpClient = httpClientFactory.CreateClient("Films");
        }

        public async Task<List<DirectorDTO>> GetDirectors()
        {
            return await httpClient.GetFromJsonAsync<List<DirectorDTO>>("Directors");
        }

        public async Task<List<MovieDTO>> GetMovies()
        {
            return await httpClient.GetFromJsonAsync<List<MovieDTO>>("Movies");
        }

        public async Task<HttpResponseMessage> CreateDirectorAsync(DirectorDTO director)
        {
            director.IsActive = true;
            return await httpClient.PostAsJsonAsync("Directors", director);

        }

        public async Task<HttpResponseMessage> CreateMovieAsync(MovieDTO movie)
        {
            return await httpClient.PostAsJsonAsync("Movies", movie);
        }

        public async Task<HttpResponseMessage> DeleteMovieAsync(int id)
        {
            return await httpClient.DeleteAsync($"Movies/{id}");
        }

        public async Task<HttpResponseMessage> UpdateMovieAsync(MovieDTO movie)
        {
            return await httpClient.PutAsJsonAsync($"Movies/{movie.MovieId}", movie);
        }
    }
}
