# Use the official .NET image as a parent image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_22.x | bash - \
    && apt-get install -y nodejs

# Set the working directory
WORKDIR /workspace

# Copy the project files into the container
COPY . .

# # Install project dependencies
# RUN dotnet restore

# Expose port 3000 for Node.js
EXPOSE 3000

# Expose port 5000 for .NET
EXPOSE 5000
