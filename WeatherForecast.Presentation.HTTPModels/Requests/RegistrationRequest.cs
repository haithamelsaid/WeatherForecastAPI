﻿namespace WeatherForecast.Presentation.HTTPModels.Requests
{
    public class RegistrationRequest
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
