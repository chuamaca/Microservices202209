FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices.APIGateway.Auth.API.csproj", "./"]
RUN dotnet restore "Microservices.APIGateway.Auth.API.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "Microservices.APIGateway.Auth.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.APIGateway.Auth.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.APIGateway.Auth.API.dll"]