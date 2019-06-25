FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore Screenshot.Processor.csproj
RUN dotnet build --no-restore Screenshot.Processor.csproj

FROM build AS publish
RUN dotnet publish --no-restore Screenshot.Processor.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Screenshot.Processor.dll"]