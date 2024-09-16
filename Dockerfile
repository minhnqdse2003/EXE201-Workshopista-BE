FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src"
COPY ["EXE201-Workshopista/EXE201-Workshopista.csproj","EXE201-Workshopista/"]
COPY ["Repository/Repository.csproj","Repository/"]
COPY ["Service/Service.csproj","Service/"]
RUN dotnet restore "EXE201-Workshopista/EXE201-Workshopista.csproj"
COPY . .
WORKDIR "/src/EXE201-Workshopista" 
RUN dotnet build "EXE201-Workshopista.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EXE201-Workshopista.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "EXE201-Workshopista.dll" ]