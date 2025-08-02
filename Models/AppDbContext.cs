using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourseProject.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Headlights> Headlights => Set<Headlights>();
        public DbSet<Tires> Tires => Set<Tires>();
        public DbSet<Person> People => Set<Person>();
        public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();

        private IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_configuration.GetConnectionString("SqlServer"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
