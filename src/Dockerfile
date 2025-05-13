# ---------------------------------------------
# Stage 1: Base Image
# ---------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV ASPNETCORE_URLS="http://+:8080;https://+:8081"
ENV ASPNETCORE_ENVIRONMENT=Development

# ---------------------------------------------
# Stage 2: Build
# ---------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy everything from /src folder into the build context
COPY . .

# Restore dependencies (nuget + project references)
RUN dotnet restore OfficePerformanceReview.sln

# Build 
RUN dotnet build OfficePerformanceReview.sln -c $BUILD_CONFIGURATION --no-restore -o /app/build
# Stage 2: Publish the API Project

FROM build AS publish
RUN dotnet publish WebAPI/OfficePerformanceReview.WebAPI.csproj  -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ---------------------------------------------
# Stage 3: Final
# ---------------------------------------------
FROM base AS runtime
WORKDIR /app
COPY --from=publish /app/publish . 
USER app
ENTRYPOINT ["dotnet", "OfficePerformanceReview.WebAPI.dll"]