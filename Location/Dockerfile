FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR ./src
COPY ["/LocationAPI/LocationAPI.csproj", "/LocationAPI/"]
COPY ["/LocationService/LocationService.csproj", "/LocationService/"]
COPY ["/LocationData/LocationData.csproj", "/LocationData/"]
COPY ["/LocationAbstraction/LocationAbstraction.csproj", "/LocationAbstraction/"]
RUN dotnet restore "/LocationAPI/LocationAPI.csproj"
COPY . .
WORKDIR "/src/LocationAPI"
RUN dotnet build "LocationAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LocationAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocationAPI.dll"]