namespace WeatherForecast.Application.DTOs.Output
{
    public class ServiceLayerOutput<T>
    {
        public bool Success { get; set; }
        public bool IsExistException { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
        public int? ErrorCode { get; set; }
        public T Data { get; set; }
    }
}
