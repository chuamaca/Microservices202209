FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices.APIGateway.Sale.API.csproj", "./"]
RUN dotnet restore "Microservices.APIGateway.Sale.API.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "Microservices.APIGateway.Sale.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.APIGateway.Sale.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.APIGateway.Sale.API.dll"]