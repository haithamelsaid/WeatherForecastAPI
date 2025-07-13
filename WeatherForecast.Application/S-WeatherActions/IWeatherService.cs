using WeatherForecast.Application.DTOs.Output;

namespace WeatherForecast.Application.S_WeatherActions
{
    public interface IWeatherService
    {
        Task<ServiceLayerOutput<WeatherOutput>> GetWeatherForCity(string city);
    }
}
