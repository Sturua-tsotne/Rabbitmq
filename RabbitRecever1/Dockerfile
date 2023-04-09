#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RabbitRecever1/RabbitReceiver1.csproj", "RabbitRecever1/"]
RUN dotnet restore "RabbitRecever1/RabbitReceiver1.csproj"
COPY . .
WORKDIR "/src/RabbitRecever1"
RUN dotnet build "RabbitReceiver1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitReceiver1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitReceiver1.dll"]