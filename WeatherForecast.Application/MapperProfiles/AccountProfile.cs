using AutoMapper;
using WeatherForecast.Application.DTOs.Input;
using WeatherForecast.Domain.Accounts;

namespace WeatherForecast.Application.MapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegistrationInput, Account>();
            CreateMap<LoginInput, Account>();
        }
    }
}
