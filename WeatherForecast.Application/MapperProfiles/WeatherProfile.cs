using AutoMapper;
using WeatherForecast.Application.DTOs.Output;
using WeatherForecast.Domain.Weathers;

namespace WeatherForecast.Application.MapperProfiles
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<Weather, WeatherOutput>();
        }
    }
}
