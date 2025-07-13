using AutoMapper;
using WeatherForecast.Application.DTOs.Output;
using WeatherForecast.Presentation.HTTPModels.Responses;

namespace WeatherForecast.Presentation.MapperProfiles
{
    public class PresentationWeatherProfile : Profile
    {
        public PresentationWeatherProfile()
        {
            CreateMap<WeatherOutput, WeatherResponse>();
        }
    }
}
