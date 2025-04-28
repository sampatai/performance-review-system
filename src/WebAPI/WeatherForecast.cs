//## This stage is used to build the service project
//# FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
//# ARG BUILD_CONFIGURATION=Release
//# WORKDIR /src
//# COPY ["OfficePerformanceReview.WebAPI/OfficePerformanceReview.WebAPI.csproj", "OfficePerformanceReview.WebAPI/"]
//# COPY ["OfficePerformanceReview.Application/OfficePerformanceReview.Application.csproj", "OfficePerformanceReview.Application/"]
//# COPY ["Domain/OfficePerformanceReview.Domain.csproj", "Domain/"]
//# COPY ["Shared/OfficePerformanceReview.Shared.csproj", "Shared/"]
//# COPY ["OfficePerformanceReview.Infrastructure/OfficePerformanceReview.Infrastructure.csproj", "OfficePerformanceReview.Infrastructure/"]
//# RUN dotnet restore "./OfficePerformanceReview.WebAPI/OfficePerformanceReview.WebAPI.csproj"
//# COPY . .
//# WORKDIR "/src/OfficePerformanceReview.WebAPI"
//# RUN dotnet build "./OfficePerformanceReview.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build
//#
//## This stage is used to publish the service project to be copied to the final stage
//# FROM build AS publish
//# ARG BUILD_CONFIGURATION=Release
//# RUN dotnet publish "./OfficePerformanceReview.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
//#
//## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
//# FROM base AS final
//# WORKDIR /app
//# COPY --from=publish /app/publish .
//# ENTRYPOINT ["dotnet", "OfficePerformanceReview.WebAPI.dll"]