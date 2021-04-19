FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ./Crawler.csproj Crawler/

RUN dotnet restore "Crawler/Crawler.csproj"
WORKDIR "/src/Crawler"
COPY . .
RUN dotnet build "Crawler.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Crawler.csproj" -c Release -o /app

FROM base AS final
# ENV ASPNETCORE_URLS http://*:5001
# ENV DATABASE_URL host=localhost;port=5432;database=spolubydlo_db;username=dominikvit;
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Crawler.dll"]
