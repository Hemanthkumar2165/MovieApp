namespace MovieDirector.DTO
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public int YearofRelease { get; set; }
        public int Rating { get; set; }
        public string? MainLanguage { get; set; }

        public List<int> DirectorIds { get; set; } = new();

    }

}
