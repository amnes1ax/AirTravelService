FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["src/AirTravelService.Migrator/AirTravelService.Migrator.csproj", "src/AirTravelService.Migrator/"]
COPY ["src/AirTravelService.DataAccess.PostgresSql/AirTravelService.DataAccess.PostgresSql.csproj", "src/AirTravelService.DataAccess.PostgresSql/"]
COPY ["src/AirTravelService.DataAccess/AirTravelService.DataAccess.csproj", "src/AirTravelService.DataAccess/"]
COPY ["src/AirTravelService.Domain/AirTravelService.Domain.csproj", "src/AirTravelService.Domain/"]
RUN dotnet restore "src/AirTravelService.Migrator/AirTravelService.Migrator.csproj"
COPY . .
WORKDIR "/src/src/AirTravelService.Migrator"
RUN dotnet build "AirTravelService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AirTravelService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirTravelService.Migrator.dll"]
