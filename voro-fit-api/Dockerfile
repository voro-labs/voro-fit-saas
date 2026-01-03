FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VoroFit.API/VoroFit.API.csproj", "VoroFit.API/"]
COPY ["VoroFit.Application/VoroFit.Application.csproj", "VoroFit.Application/"]
COPY ["VoroFit.Contract/VoroFit.Contract.csproj", "VoroFit.Contract/"]
COPY ["VoroFit.Domain/VoroFit.Domain.csproj", "VoroFit.Domain/"]
COPY ["VoroFit.Infrastructure/VoroFit.Infrastructure.csproj", "VoroFit.Infrastructure/"]
COPY ["VoroFit.Shared/VoroFit.Shared.csproj", "VoroFit.Shared/"]
RUN dotnet restore "VoroFit.API/VoroFit.API.csproj"
COPY . .
WORKDIR "/src/VoroFit.API"
RUN dotnet build "VoroFit.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VoroFit.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VoroFit.API.dll"]
