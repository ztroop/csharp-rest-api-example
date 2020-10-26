FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy .csproj and restore as distinct layers.
COPY *.sln .
COPY RestAPIExample/*.csproj ./RestAPIExample/
RUN dotnet restore
RUN dotnet tool install --global dotnet-ef

# Copy everything else and build app.
COPY RestAPIExample/. ./RestAPIExample/
COPY ./start.sh ./RestAPIExample/
WORKDIR /app/RestAPIExample
RUN chmod +x ./start.sh

CMD /bin/bash ./start.sh