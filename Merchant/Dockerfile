FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["/MerchantApi/MerchantApi.csproj", "/MerchantApi/"]
COPY ["/MerchantService/MerchantService.csproj", "/MerchantService/"]
COPY ["/MerchantData/MerchantData.csproj", "/MerchantData/"]
COPY ["/MerchantAbstraction/MerchantAbstraction.csproj", "/MerchantAbstraction/"]
RUN dotnet restore "/MerchantApi/MerchantApi.csproj"
COPY . .
WORKDIR "/src/MerchantApi"
RUN dotnet build "MerchantApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MerchantApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MerchantApi.dll"]