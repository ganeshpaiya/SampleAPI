FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["/LookUpApi/LookUpApi.csproj", "/LookUpApi/"]
COPY ["/LookUpData/LookUpData.csproj", "/LookUpData/"]
COPY ["/LookUpService/LookUpService.csproj", "/LookUpService/"]
COPY ["/LookUpAbstraction/LookUpAbstraction.csproj", "/LookUpAbstraction/"]
RUN dotnet restore "/LookUpApi/LookUpApi.csproj"
COPY . .
WORKDIR "/src/LookUpApi"
RUN dotnet build "LookUpApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LookUpApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LookUpApi.dll"]