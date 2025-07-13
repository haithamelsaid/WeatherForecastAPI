using WeatherForecast.Application.DTOs.Input;
using WeatherForecast.Presentation.HTTPModels.Requests;
using WeatherForecast.Presentation.HTTPModels.Responses;
using WeatherForecast.Presentation.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WeatherForecast.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleFailure(bool isHasException, IEnumerable<string> errorMessages)
        {
            if (isHasException)
                return StatusCode(500, new FailedResponse
                {
                    Errors = ControllerSharedOperations.FAILED_MESSAGE
                });

            return BadRequest(new FailedResponse { Errors = string.Join(" \n ", errorMessages) });
        }

        protected IActionResult HandleSuccessQuery<T>(T response)
        {
            return Ok(new BaseResponse<T>
            {
                Data = response
            });
        }

        protected IActionResult HandleSuccessAction()
        {
            return Ok(new BaseResponse<string>
            {
                Data =  ControllerSharedOperations.SUCCESS_MESSAGE 
            });
        }

    }
}
