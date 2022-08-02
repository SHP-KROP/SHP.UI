FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5002

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["OnlineShopAPI.csproj", "OnlineShopAPI/"]
RUN dotnet restore "OnlineShopAPI\OnlineShopAPI.csproj"
WORKDIR "/src/OnlineShopAPI"
COPY . .
RUN dotnet build "OnlineShopAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineShopAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineShopAPI.dll"]
