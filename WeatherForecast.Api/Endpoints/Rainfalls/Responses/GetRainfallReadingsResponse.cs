using WeatherForecast.Core.Models;

namespace WeatherForecast.Api.Endpoints.Rainfalls.Responses;

public class GetRainfallReadingsResponse
{
    public required IEnumerable<RainfallReadings> Readings { get; set; }
}