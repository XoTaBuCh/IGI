FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WEB_053501_Sauchuk/WEB_053501_Sauchuk.csproj", "WEB_053501_Sauchuk/"]
RUN dotnet restore "WEB_053501_Sauchuk/WEB_053501_Sauchuk.csproj"
COPY . .
WORKDIR "/src/WEB_053501_Sauchuk"
RUN dotnet build "WEB_053501_Sauchuk.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WEB_053501_Sauchuk.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WEB_053501_Sauchuk.dll"]
