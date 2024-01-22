using MediatR;
using WeatherForecast.Core.Models;

namespace WeatherForecast.UseCases.Rainfalls.Queries;

public record GetRainfallReadingsQuery : IRequest<IEnumerable<RainfallReadings>>
{
    public int StationId { get; init; }
    public int Count { get; init; }
}