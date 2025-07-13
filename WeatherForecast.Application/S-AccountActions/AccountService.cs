using AutoMapper;
using WeatherForecast.Application.DTOs.Input;
using WeatherForecast.Application.DTOs.Output;
using WeatherForecast.Domain.Accounts;
using WeatherForecast.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace WeatherForecast.Application.S_AccountActions
{
    public class AccountService(IUnitOfWork unitOfWork, UserManager<Account> userManager, SignInManager<Account> signInManager,IMapper mapper) : IAccountService
    {
        public readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<Account> _userManager = userManager;
        private readonly SignInManager<Account> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceLayerOutput<bool>> Register(RegistrationInput registrationInput)
        {
            try
            {
                #region Validate Inputs

                if (registrationInput == null || string.IsNullOrEmpty(registrationInput.UserName) || string.IsNullOrEmpty(registrationInput.Email) || string.IsNullOrEmpty(registrationInput.PhoneNumber) || string.IsNullOrEmpty(registrationInput.Password))
                {
                    return new ServiceLayerOutput<bool>
                    {
                        ErrorMessages = ["Invalid Inputs"]
                    };
                }

                if (!Regex.IsMatch(registrationInput.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    return new ServiceLayerOutput<bool>
                    {
                        ErrorMessages = ["Email is not valid"]
                    };

                #endregion

                Account account = _mapper.Map<Account>(registrationInput);

                var result = await _userManager.CreateAsync(account, registrationInput.Password);

                if (!result.Succeeded)
                    return new ServiceLayerOutput<bool>
                    {
                        ErrorMessages = ["Registration failed"]
                    };

                await _userManager.AddToRoleAsync(account, "user");

                return new ServiceLayerOutput<bool>
                {
                    Success = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                return new ServiceLayerOutput<bool>
                {
                    IsExistException = true,
                    ErrorMessages = new[] { ex.ToString() }
                };
            }
        }
        public async Task<ServiceLayerOutput<string>> Login(LoginInput loginInput)
        {
            try
            {
                #region Validate Inputs

                if (loginInput == null || string.IsNullOrEmpty(loginInput.Email) || string.IsNullOrEmpty(loginInput.Password))
                {
                    return new ServiceLayerOutput<string>
                    {
                        ErrorMessages = ["Invalid Inputs"]
                    };
                }

                #endregion

                Account account = await _unitOfWork.AccountRepository.GetAccountByEmail(loginInput.Email);
                
                if(account == null)
                    return new ServiceLayerOutput<string>
                    {
                        ErrorMessages = ["Invalid username or password."]
                    };

                var result = await _signInManager.PasswordSignInAsync(account, loginInput.Password, true, false);

                if (result.IsLockedOut)
                    return new ServiceLayerOutput<string>
                    {
                        ErrorMessages = ["User account is locked out."]
                    };

                if (!result.Succeeded)
                    return new ServiceLayerOutput<string>
                    {
                        ErrorMessages = ["Invalid username or password."]
                    };

                return new ServiceLayerOutput<string>
                {
                    Success = true,
                    Data = account.Id
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                return new ServiceLayerOutput<string>
                {
                    IsExistException = true,
                    ErrorMessages = new[] { ex.ToString() }
                };
            }
        }
    }
}
