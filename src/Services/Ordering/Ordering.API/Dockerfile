#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Ordering/Ordering.API/Ordering.API.csproj", "Services/Ordering/Ordering.API/"]
COPY ["Services/Ordering/Ordering.Application/Ordering.Application.csproj", "Services/Ordering/Ordering.Application/"]
COPY ["Services/Ordering/Ordering.Domain/Ordering.Domain.csproj", "Services/Ordering/Ordering.Domain/"]
COPY ["Contracts/EventBus.Messages/EventBus.Messages.csproj", "Contracts/EventBus.Messages/"]
COPY ["Contracts/Common.Logging/Common.Logging.csproj", "Contracts/Common.Logging/"]
COPY ["Services/Ordering/Infra/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Ordering/Infra/Ordering.Infrastructure/"]
COPY ["Services/Ordering/Infra/CrossCutting/Ordering.CrossCutting.Bus/Ordering.CrossCutting.Bus.csproj", "Services/Ordering/Infra/CrossCutting/Ordering.CrossCutting.Bus/"]
RUN dotnet restore "Services/Ordering/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]