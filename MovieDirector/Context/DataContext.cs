using Microsoft.EntityFrameworkCore;
using MovieDirector.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MovieDirector.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Movie> movies { get; set; }

        public DbSet<Director> directors { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
