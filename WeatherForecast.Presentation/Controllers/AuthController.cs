using AutoMapper;
using WeatherForecast.Application.DTOs.Input;
using WeatherForecast.Application.S_AccountActions;
using WeatherForecast.Presentation.HTTPModels.Requests;
using WeatherForecast.Presentation.HTTPModels.Responses;
using WeatherForecast.Presentation.Services.S_JwtServices;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAccountService accountService, IMapper mapper, IJWTService jWTService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IMapper _mapper = mapper;
        private readonly IJWTService _jWTService = jWTService;

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationRequest registrationRequest)
        {
            var response = await _accountService.Register(_mapper.Map<RegistrationInput>(registrationRequest));

            if (response.IsExistException)
                return StatusCode(500, new FailedResponse
                {
                    Errors = "There Exist Something Wrong, try it again later"
                });

            if (!response.Success)
                return BadRequest(new FailedResponse { Errors = string.Join(" \n ", response.ErrorMessages) });

            return Ok("User registered successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest accountLogin)
        {
            LoginInput loginInput = _mapper.Map<LoginInput>(accountLogin);
            var response = await _accountService.Login(loginInput);

            if (response.IsExistException)
                return StatusCode(500, new FailedResponse
                {
                    Errors = "There Exist Something Wrong, try it again later"
                });

            if (!response.Success)
                return BadRequest(new FailedResponse { Errors = string.Join(" \n ", response.ErrorMessages) });

            return Ok(new { token = GenerateJwtToken(response.Data) });
        }
         

        #region Private Methods

        private string GenerateJwtToken(string accountId)
        {
            return _jWTService.GenerateToken(accountId);
        }

        #endregion
    }
}
