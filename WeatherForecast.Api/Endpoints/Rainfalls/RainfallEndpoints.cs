using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Endpoints.Responses;
using WeatherForecast.UseCases.Rainfalls.Queries;

namespace WeatherForecast.Api.Endpoints.Rainfalls;

public static class RainfallEndpoints
{
    public static RouteGroupBuilder MapRainfallEndpoints(this RouteGroupBuilder route)
    {
        var rainfallGroup = route
            .MapGroup("/rainfall")
            .WithTags("Rainfall");

        rainfallGroup.MapGet("/id/{stationId:min(1):max(100)}/readings", GetReadings)
            .WithDescription("Retrieve the latest readings for the specified stationId");

        return route;
    }

    private static async Task<Ok<GetRainfallReadingsResponse>> GetReadings(
        [FromServices] ISender sender,
        [FromRoute] int stationId,
        [FromQuery] int count = 10
    )
    {
        var request = new GetRainfallReadingsQuery
        {
            Count = count,
            StationId = stationId
        };

        var readings = await sender.Send(request);

        var response = new GetRainfallReadingsResponse
        {
            Readings = readings
        };
        return TypedResults.Ok(response);
    }
}