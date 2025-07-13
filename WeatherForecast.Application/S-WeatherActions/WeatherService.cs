using AutoMapper;
using WeatherForecast.Application.DTOs.Output;
using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Domain.Weathers;

namespace WeatherForecast.Application.S_WeatherActions
{
    public class WeatherService(IUnitOfWork unitOfWork, IMapper mapper) : IWeatherService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceLayerOutput<WeatherOutput>> GetWeatherForCity(string city)
        {
            try
            {
                #region Validation Inputs

                if (string.IsNullOrEmpty(city))
                    return new ServiceLayerOutput<WeatherOutput>
                    {
                        ErrorMessages = ["Invalid input"]
                    };

                #endregion

                Weather weather = await _unitOfWork.WeatherRepository.GetByCity(city);

                if(weather is null)
                    return new ServiceLayerOutput<WeatherOutput>
                    {
                        ErrorMessages = ["City is not exist"]
                    };

                return new ServiceLayerOutput<WeatherOutput>
                {
                    Data = _mapper.Map<WeatherOutput>(weather),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceLayerOutput<WeatherOutput>
                {
                    IsExistException = true,
                    ErrorMessages = new[] { ex.ToString() }
                };
            }
        }

    }
}
