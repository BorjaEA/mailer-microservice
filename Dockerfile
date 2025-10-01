# -----------------
# Build Stage
# -----------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app

# -----------------
# Runtime Stage
# -----------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app ./

# Expose ports here (metadata for Docker)
EXPOSE 8080
EXPOSE 8081

# Run the app
ENTRYPOINT ["dotnet", "mailer-microservice.dll"]
