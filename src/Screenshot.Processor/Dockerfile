FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/src/Screenshot.Processor
RUN dotnet restore
RUN dotnet build --no-restore

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app

COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Screenshot.Processor.dll"]