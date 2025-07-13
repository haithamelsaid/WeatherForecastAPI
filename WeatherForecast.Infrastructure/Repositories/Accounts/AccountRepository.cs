using WeatherForecast.Domain.Accounts;
using WeatherForecast.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace WeatherForecast.Infrastructure.Repositories.Accounts
{
    public class AccountRepository(WeatherDbContext context) : IAccountRepository
    {
        private readonly WeatherDbContext _context = context;

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
