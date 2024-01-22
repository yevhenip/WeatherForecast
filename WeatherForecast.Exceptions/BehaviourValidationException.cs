namespace WeatherForecast.Exceptions;

public class BehaviourValidationException : DomainException
{
    public override string Code => "ValidationError";

    public BehaviourValidationException(Dictionary<string, string[]> errors)
        : base("Validation error occured")
    {
        Details = new Dictionary<string, object?>();

        foreach (var (key, value) in errors)
        {
            Details.Add(key, value);
        }
    }
}