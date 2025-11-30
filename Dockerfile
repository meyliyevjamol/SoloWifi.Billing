FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution
COPY SoloWifi.Billing.sln .

# Copy all project files
COPY SoloWifi.Billing.WebApi/SoloWifi.Billing.WebApi.csproj SoloWifi.Billing.WebApi/
COPY lib/SoloWifi.Billing.ServiceLayer/SoloWifi.Billing.ServiceLayer.csproj lib/SoloWifi.Billing.ServiceLayer/
COPY lib/SoloWifi.Billing.DataLayer/SoloWifi.Billing.DataLayer.csproj lib/SoloWifi.Billing.DataLayer/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Publish WebApi
RUN dotnet publish SoloWifi.Billing.WebApi/SoloWifi.Billing.WebApi.csproj -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "SoloWifi.Billing.WebApi.dll"]


