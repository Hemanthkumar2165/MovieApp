namespace MovieDirector.DTO
{
    public class DirectorDTO
    {
        public int DirectorId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public List<MovieDTO> movies { get; set; } = new();

    }

}
