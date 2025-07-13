namespace WeatherForecast.Domain.Weathers
{
    public interface IWeatherRepository
    {
        Task<Weather> GetByCity(string city);
    }
}
