FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["src/AirTravelService.Api/AirTravelService.Api.csproj", "src/AirTravelService.Api/"]
COPY ["src/AirTravelService.Service/AirTravelService.Service.csproj", "src/AirTravelService.Service/"]
COPY ["src/AirTravelService.DataAccess/AirTravelService.DataAccess.csproj", "src/AirTravelService.DataAccess/"]
COPY ["src/AirTravelService.Domain/AirTravelService.Domain.csproj", "src/AirTravelService.Domain/"]
COPY ["src/AirTravelService.ReadModel/AirTravelService.ReadModel.csproj", "src/AirTravelService.ReadModel/"]
COPY ["src/AirTravelService.DataAccess.PostgresSql/AirTravelService.DataAccess.PostgresSql.csproj", "src/AirTravelService.DataAccess.PostgresSql/"]
RUN dotnet restore "src/AirTravelService.Api/AirTravelService.Api.csproj"
COPY . .
WORKDIR "/src/src/AirTravelService.Api"
RUN dotnet build "AirTravelService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AirTravelService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirTravelService.Api.dll"]
