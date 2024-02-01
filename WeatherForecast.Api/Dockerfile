FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WeatherForecast.Api/WeatherForecast.Api.csproj", "WeatherForecast.Api/"]
COPY ["WeatherForecast.Core/WeatherForecast.Core.csproj", "WeatherForecast.Core/"]
COPY ["WeatherForecast.Exceptions/WeatherForecast.Exceptions.csproj", "WeatherForecast.Exceptions/"]
COPY ["WeatherForecast.UseCases/WeatherForecast.UseCases.csproj", "WeatherForecast.UseCases/"]
RUN dotnet restore "WeatherForecast.Api/WeatherForecast.Api.csproj"
COPY . .
WORKDIR "/src/WeatherForecast.Api"
RUN dotnet build "WeatherForecast.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WeatherForecast.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherForecast.Api.dll"]
