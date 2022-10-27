FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Serilog.Example.API/Serilog.Example.API.csproj", "Serilog.Example.API/"]
RUN dotnet restore "Serilog.Example.API/Serilog.Example.API.csproj"
COPY . .
WORKDIR "/src/Serilog.Example.API"
RUN dotnet build "Serilog.Example.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Serilog.Example.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Serilog.Example.API.dll"]
