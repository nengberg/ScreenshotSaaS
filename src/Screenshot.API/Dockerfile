FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore Screenshot.API.csproj
RUN dotnet build --no-restore Screenshot.API.csproj

FROM build AS publish
RUN dotnet publish --no-restore Screenshot.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Screenshot.API.dll"]