namespace FilmClient.Model
{
    public class DTOs
    {
        public class DirectorDTO
        {
            public int DirectorId { get; set; }
            public string Name { get; set; }

            public bool IsActive { get; set; }

            public List<MovieDTO> movies { get; set; }
        }

        public class MovieDTO
        {
            public int MovieId { get; set; }
            public string? Title { get; set; }
            public int YearofRelease { get; set; }
            public int Rating { get; set; }
            public string? MainLanguage { get; set; }

            public List<int> DirectorIds { get; set; } = new();
        }

        public class MovieDirectorGet
        {
            public int MovieId { get; set; }
            public string? Title { get; set; }
            public int YearofRelease { get; set; }
            public int Rating { get; set; }
            public string? MainLanguage { get; set; }
        }
    }
}
