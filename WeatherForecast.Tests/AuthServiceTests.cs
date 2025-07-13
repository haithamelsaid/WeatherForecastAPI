using Microsoft.AspNetCore.Identity;
using Moq;
using WeatherForecast.Domain.Accounts;
using WeatherForecast.Application.S_AccountActions;
using WeatherForecast.Application.DTOs.Input;
using WeatherForecast.Application.DTOs.Output;

namespace WeatherForecast.Tests.UnitTests
{
    public class AuthServiceTests(IAccountService accountService)
    {
        private readonly Mock<UserManager<Account>> _mockUserManager = new();
        private readonly IAccountService _accountService = accountService;

        [Fact]
        public async Task RegisterUserAsync_ValidUser_ReturnsTrue()
        {
            string userName = "haithamelsaid";
            string phoneNumber = "+201220153969";
            string email = "haithamelsaid9@gmail.com";
            string password = "Hh111111***";
            RegistrationInput account = new() { Email = email, PhoneNumber = phoneNumber, Password = password, UserName = userName };

            // Fix: Provide required arguments to RegisterTokenProvider  
            _mockUserManager.Setup(s => s.RegisterTokenProvider(It.IsAny<string>(), It.IsAny<IUserTwoFactorTokenProvider<Account>>()))
                            .Verifiable();

            var result = await _accountService.Register(account);

            Assert.True(result.Data);

            _mockUserManager.Verify(m => m.RegisterTokenProvider(It.IsAny<string>(), It.IsAny<IUserTwoFactorTokenProvider<Account>>()), Times.Once);
        }
    }
}