using WeatherForecast.Domain.Accounts;
using WeatherForecast.Domain.Weathers;

namespace WeatherForecast.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        IWeatherRepository WeatherRepository { get; }

        Task Complete();
    }
}
