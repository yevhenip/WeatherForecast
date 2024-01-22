using WeatherForecast.Api.Endpoints.Rainfalls;
using WeatherForecast.Api.Middleware;
using WeatherForecast.UseCases;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddApplicationLayer();

services.AddCors();

services.AddScoped<ExceptionHandlerMiddleware>();
services.AddSingleton<HttpClient>(_ => new HttpClient());

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

var apiGroup = app.MapGroup("/");
apiGroup.MapRainfallEndpoints();

app.UseCors(x => x
    .SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithExposedHeaders("*"));

app.Run();