using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDirector.Model
{
    [Table("Director")]
    public class Director
    {
        [Key]
        public int DirectorId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public List<Movie> Movies { get; set; } = new();
    }
}
