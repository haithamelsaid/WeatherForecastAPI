using WeatherForecast.Domain.Accounts;
using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Domain.Weathers;
using WeatherForecast.Infrastructure.Context;
using WeatherForecast.Infrastructure.Repositories.Accounts;
using WeatherForecast.Infrastructure.Repositories.Weathers;

namespace WeatherForecast.Infrastructure.Repositories.Base
{
    public class UnitOfWork(WeatherDbContext context) : IUnitOfWork
    {
        private readonly WeatherDbContext _context = context;

        private IAccountRepository _iAccountRepository;
        public IAccountRepository AccountRepository
        {
            get
            {
                return _iAccountRepository ??= new AccountRepository(_context);
            }
        }

        private IWeatherRepository _iWeatherRepository;
        public IWeatherRepository WeatherRepository
        {
            get
            {
                return _iWeatherRepository ??= new WeatherRepository(_context);
            }
        }
        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        #endregion
    }
}
