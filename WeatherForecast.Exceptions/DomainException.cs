namespace WeatherForecast.Exceptions;

public abstract class DomainException : Exception
{
    public abstract string Code { get; }
    public IDictionary<string, object?> Parameters => Details ??= new Dictionary<string, object?>();

    protected Dictionary<string, object?>? Details { get; set; }

    protected DomainException(string message) : base(message)
    {
    }
}