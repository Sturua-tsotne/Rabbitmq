#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RabbitSender/RabbitSender.csproj", "RabbitSender/"]
RUN dotnet restore "RabbitSender/RabbitSender.csproj"
COPY . .
WORKDIR "/src/RabbitSender"
RUN dotnet build "RabbitSender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitSender.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitSender.dll"]