# Stage 1: Restore and Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Install Azure CLI and PowerShell in build stage (needed for some NuGet packages)
RUN apt-get update && \
    apt-get install -y wget apt-transport-https software-properties-common && \
    wget -q https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y powershell azure-cli

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

# Install Azure CLI and PowerShell in runtime image
RUN apt-get update && \
    apt-get install -y wget apt-transport-https software-properties-common && \
    wget -q https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y powershell azure-cli

# Copy published output
COPY --from=build /app/publish .

# Set environment variables for Azure authentication (optional)
# ENV AZURE_TENANT_ID=your_tenant_id
# ENV AZURE_CLIENT_ID=your_client_id
# ENV AZURE_CLIENT_SECRET=your_client_secret

ENTRYPOINT ["dotnet", "OfficePerformanceReview.WebAPI.dll"]