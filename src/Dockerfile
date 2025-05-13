# ---------------------------------------------
# Stage 1: Build Stage
# ---------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution file and project files
COPY . .

# Restore dependencies
RUN dotnet restore OfficePerformanceReview.sln

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet build OfficePerformanceReview.sln -c $BUILD_CONFIGURATION --no-restore -o /app/build

# Publish the application
RUN dotnet publish WebAPI/OfficePerformanceReview.WebAPI.csproj -c $BUILD_CONFIGURATION --no-restore -o /app/publish /p:IncludeSymbols=false /p:DebugType=None

# ---------------------------------------------
# Stage 2: Final Runtime Stage
# ---------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Create a non-root user for security
USER app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Define entrypoint
ENTRYPOINT ["dotnet", "OfficePerformanceReview.WebAPI.dll"]

# Expose the application port (HTTP by default in development)
EXPOSE 80