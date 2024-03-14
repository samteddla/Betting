#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["SportBet.Api/SportBet.Api.csproj", "SportBet.Api/"]
COPY ["SportBet.Application/SportBet.Application.csproj", "SportBet.Application/"]
COPY ["SportBet.Domain/SportBet.Domain.csproj", "SportBet.Domain/"]
COPY ["SportBet.Contracts/SportBet.Contracts.csproj", "SportBet.Contracts/"]
COPY ["SportBet.Infrastructure/SportBet.Infrastructure.csproj", "SportBet.Infrastructure/"]

RUN dotnet restore "./SportBet.Api/SportBet.Api.csproj"
COPY . .
WORKDIR "/src/SportBet.Api"
RUN dotnet build "./SportBet.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SportBet.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportBet.Api.dll"]