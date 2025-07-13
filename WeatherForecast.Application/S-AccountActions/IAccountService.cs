using WeatherForecast.Application.DTOs.Input;
using WeatherForecast.Application.DTOs.Output;

namespace WeatherForecast.Application.S_AccountActions
{
    public interface IAccountService
    {
        Task<ServiceLayerOutput<bool>> Register(RegistrationInput registrationInput);
        Task<ServiceLayerOutput<string>> Login(LoginInput loginInput);
    }
}
