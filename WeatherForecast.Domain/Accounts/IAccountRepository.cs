namespace WeatherForecast.Domain.Accounts
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByEmail(string email);
    }
}
