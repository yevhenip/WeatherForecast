namespace WeatherForecast.Exceptions;

public class HttpClientException() : DomainException("Http error occured")
{
    public override string Code => "HttpClientError";
}