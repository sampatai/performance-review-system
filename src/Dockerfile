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


ENTRYPOINT ["dotnet", "OfficePerformanceReview.WebAPI.dll"]

#docker build -t office-performance-review:latest .
#docker tag office-performance-review:latest yourdockerhubusername/office-performance-review:latest
#docker push yourdockerhubusername/office-performance-review:latest
