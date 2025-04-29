# Stage 1: Restore and Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY OfficePerformanceReview.sln . 
COPY . .

# Restore dependencies
RUN dotnet restore

# Copy the full source code
COPY . .

WORKDIR /src/WebAPI

# Publish the app to a folder
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "OfficePerformanceReview.WebAPI.dll"]
