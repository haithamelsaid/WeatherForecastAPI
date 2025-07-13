using WeatherForecast.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Weathers;

namespace WeatherForecast.Infrastructure.Repositories.Weathers
{
    public class WeatherRepository(WeatherDbContext context) : IWeatherRepository
    {
        private readonly WeatherDbContext _context = context;

        public async Task<Weather> GetByCity(string city)
        {
            return await _context.Weathers
                .Where(w => w.City == city)
                .Select(w => 
                    new Weather {
                        WeatherForecast = w.WeatherForecast,
                        HighTemp = w.HighTemp,
                        LowTemp = w.LowTemp
                })
                .FirstOrDefaultAsync();
        }
    }
}
