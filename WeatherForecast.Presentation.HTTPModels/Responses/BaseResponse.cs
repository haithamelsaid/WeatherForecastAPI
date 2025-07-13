namespace WeatherForecast.Presentation.HTTPModels.Responses
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
    }
}
