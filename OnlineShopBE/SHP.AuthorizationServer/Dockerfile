FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY SHP.AuthorizationServer.Web/*.csproj ./
RUN dotnet restore

COPY SHP.AuthorizationServer.Web/. ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SHP.AuthorizationServer.Web.dll"]