using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using MovieDirector.Context;

namespace MovieDirector.Model
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public int YearofRelease { get; set; }

        public int Rating { get; set; }

        public string? MainLanguage { get; set; }

        public List<Director> Directors { get; set; } = new();
    }
}