namespace WeatherForecast.Exceptions;

public class EntityNotFoundException : DomainException
{
    public override string Code => "EntityNotFound";

    public EntityNotFoundException(string entityName, object? key = null)
        : base($"{entityName} doesn't exist")
    {
        Details = new Dictionary<string, object?>
        {
            ["entityName"] = entityName
        };

        if (key != null)
            Details["key"] = key;
    }
}