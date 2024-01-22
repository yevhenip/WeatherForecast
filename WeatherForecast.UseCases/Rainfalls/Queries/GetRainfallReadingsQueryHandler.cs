using System.Text.Json;
using System.Text.Json.Serialization;
using MapsterMapper;
using MediatR;
using WeatherForecast.Core.Models;
using WeatherForecast.Exceptions;

namespace WeatherForecast.UseCases.Rainfalls.Queries;

public class GetRainfallReadingsQueryHandler(HttpClient client, IMapper mapper)
    : IRequestHandler<GetRainfallReadingsQuery, IEnumerable<RainfallReadings>>
{
    public async Task<IEnumerable<RainfallReadings>> Handle(GetRainfallReadingsQuery request,
        CancellationToken cancellationToken)
    {
        var url =
            $"http://environment.data.gov.uk/flood-monitoring/id/stations/{request.StationId}/readings?_limit={request.Count}";
        var response = await client.GetAsync(url, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new HttpClientException();

        var result = await response.Content.ReadAsStreamAsync(cancellationToken);
        var readings =
            await JsonSerializer.DeserializeAsync<JsonReadings>(result, cancellationToken: cancellationToken);

        if (readings is null || !readings.Items.Any())
            throw new EntityNotFoundException("Station", request.StationId);

        return mapper.Map<IEnumerable<RainfallReadings>>(readings.Items);
    }

    private class JsonReadings
    {
        [JsonPropertyName("items")] public required IEnumerable<JsonReading> Items { get; set; }

        public class JsonReading
        {
            [JsonPropertyName("value")] public double AmountMeasured { get; set; }

            [JsonPropertyName("dateTime")] public DateTime DateMeasured { get; set; }
        }
    }
}