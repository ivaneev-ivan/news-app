FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["News App/News App.csproj", "News App/"]
RUN dotnet restore "News App/News App.csproj"
COPY . .
WORKDIR "/src/News App"
RUN dotnet build "News App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "News App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "News App.dll"]
