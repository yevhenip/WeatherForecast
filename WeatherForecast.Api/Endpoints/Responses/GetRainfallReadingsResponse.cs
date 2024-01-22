using WeatherForecast.Core.Models;

namespace WeatherForecast.Api.Endpoints.Responses;

public class GetRainfallReadingsResponse
{
    public required IEnumerable<RainfallReadings> Readings { get; set; }
}