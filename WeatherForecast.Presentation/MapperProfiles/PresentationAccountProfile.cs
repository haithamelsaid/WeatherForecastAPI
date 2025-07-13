using AutoMapper;
using WeatherForecast.Application.DTOs.Input;
using WeatherForecast.Presentation.HTTPModels.Requests;

namespace WeatherForecast.Presentation.MapperProfiles
{
    public class PresentationAccountProfile : Profile
    {
        public PresentationAccountProfile()
        {
            CreateMap<RegistrationRequest, RegistrationInput>();
            CreateMap<LoginRequest, LoginInput>();
        }
    }
}
