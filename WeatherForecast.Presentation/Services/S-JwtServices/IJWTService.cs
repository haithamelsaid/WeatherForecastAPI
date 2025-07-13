namespace WeatherForecast.Presentation.Services.S_JwtServices
{
    public interface IJWTService
    {
        string GenerateToken(string id);
    }
}
