FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CustomerProfileCenter.Api/CustomerProfileCenter.Api.csproj", "CustomerProfileCenter.Api/"]
RUN dotnet restore "CustomerProfileCenter.Api/CustomerProfileCenter.Api.csproj"
COPY . .
WORKDIR "/src/CustomerProfileCenter.Api"
RUN dotnet build "CustomerProfileCenter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerProfileCenter.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerProfileCenter.Api.dll"]
