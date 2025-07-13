using AutoMapper;
using WeatherForecast.Presentation.HTTPModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.S_WeatherActions;
using Microsoft.Extensions.Caching.Memory;

namespace WeatherForecast.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController(IMapper mapper,IWeatherService weatherService, IMemoryCache memoryCache) : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IWeatherService _weatherService = weatherService;
        private readonly IMemoryCache _memoryCache = memoryCache;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string city)
        {
            string cacheKey = $"WeatherFor{city}";

            if (_memoryCache.TryGetValue(cacheKey, out WeatherResponse weatherResponseCached))
                return Ok(new BaseResponse<WeatherResponse>
                {
                    Data = weatherResponseCached
                });

            await Task.Delay(500);

            var response = await _weatherService.GetWeatherForCity(city);

            if (!response.Success)
                return HandleFailure(response.IsExistException, response.ErrorMessages);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(20))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(100))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1);

            _memoryCache.Set(cacheKey, _mapper.Map<WeatherResponse>(response.Data), cacheEntryOptions);

            return Ok(new BaseResponse<WeatherResponse>
            {
                Data = _mapper.Map<WeatherResponse>(response.Data)
            });
        }
    }
}
