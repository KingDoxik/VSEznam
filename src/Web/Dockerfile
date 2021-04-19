FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ./Web.csproj Web/

RUN dotnet restore "Web/Web.csproj"
WORKDIR "/src/Web"
COPY . .
RUN dotnet build "Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app

FROM base AS final
# ENV ASPNETCORE_URLS http://*:5001
# ENV DATABASE_URL host=localhost;port=5432;database=spolubydlo_db;username=dominikvit;
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Web.dll"]
