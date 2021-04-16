#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# https://hub.docker.com/_/microsoft-dotnet-runtime
# https://hub.docker.com/_/microsoft-dotnet-sdk
FROM mcr.microsoft.com/dotnet/runtime:5.0.5-alpine3.13-amd64 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0.202-alpine3.13-amd64 AS build
WORKDIR /src
COPY ["src/WebhookController/WebhookController.csproj", "src/WebhookController/"]
RUN dotnet restore "src/WebhookController/WebhookController.csproj"
COPY . .
WORKDIR "/src/src/WebhookController"
RUN dotnet build "WebhookController.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebhookController.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebhookController.dll"]
