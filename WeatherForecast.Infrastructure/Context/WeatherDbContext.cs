using WeatherForecast.Domain.Accounts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Weathers;

namespace WeatherForecast.Infrastructure.Context
{
    public class WeatherDbContext : IdentityDbContext<Account>
    {
        public WeatherDbContext()
        {
            
        }
        public WeatherDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Weather> Weathers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
