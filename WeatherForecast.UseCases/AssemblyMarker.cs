using System.Reflection;

namespace WeatherForecast.UseCases;

public static class AssemblyMarker
{
    public static Assembly Assembly => typeof(AssemblyMarker).Assembly;
}