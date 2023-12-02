FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebAPITask/WebAPITask.csproj", "WebAPITask/"]
COPY ["BusinessAccessLayer/BusinessAccessLayer.csproj", "BusinessAccessLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
RUN dotnet restore "WebAPITask/WebAPITask.csproj"
COPY . .
WORKDIR "/src/WebAPITask"
RUN dotnet build "WebAPITask.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebAPITask.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebAPITask.dll"]