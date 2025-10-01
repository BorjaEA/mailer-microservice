# -----------------
# Build Stage
# -----------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY src/MailerMicroservice/*.csproj ./ 
RUN dotnet restore

# Copy all source code and publish
COPY src/MailerMicroservice/. ./
RUN dotnet publish -c Release -o /app

# -----------------
# Runtime Stage
# -----------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app ./

# Expose HTTP and HTTPS ports
EXPOSE 8080
EXPOSE 8081

# Set environment variable for container
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Run the app
ENTRYPOINT ["dotnet", "mailer-microservice.dll"]
