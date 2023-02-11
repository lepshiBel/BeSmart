FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BeSmart.WebApi/BeSmart.WebApi.csproj", "BeSmart.WebApi/"]
COPY ["BeSmart.Persistence/BeSmart.Persistence.csproj", "BeSmart.Persistence/"]
COPY ["BeSmart.Application/BeSmart.Application.csproj", "BeSmart.Application/"]
COPY ["BeSmart.Domain/BeSmart.Domain.csproj", "BeSmart.Domain/"]
RUN dotnet restore "BeSmart.WebApi/BeSmart.WebApi.csproj"
COPY . .
WORKDIR "/src/BeSmart.WebApi"
RUN dotnet build "BeSmart.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeSmart.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeSmart.WebApi.dll"]