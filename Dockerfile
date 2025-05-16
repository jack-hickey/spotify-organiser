# Use official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -o out

# Use ASP.NET runtime to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Run the app
ENTRYPOINT ["dotnet", "site.dll"]