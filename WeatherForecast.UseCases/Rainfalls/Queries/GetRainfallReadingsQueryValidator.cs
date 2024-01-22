using FluentValidation;

namespace WeatherForecast.UseCases.Rainfalls.Queries;

public class GetRainfallReadingsQueryValidator : AbstractValidator<GetRainfallReadingsQuery>
{
    public GetRainfallReadingsQueryValidator()
    {
        RuleFor(r => r.Count).InclusiveBetween(1, 100);
        RuleFor(r => r.StationId).InclusiveBetween(1, 100);
    }
}