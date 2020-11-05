FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy .csproj and restore as distinct layers.
COPY CSharpRESTDemo/*.csproj ./CSharpRESTDemo/
RUN dotnet restore CSharpRESTDemo
RUN dotnet tool install --global dotnet-ef

# Copy everything else and build app.
COPY CSharpRESTDemo/. ./CSharpRESTDemo/
COPY ./start.sh ./CSharpRESTDemo/
WORKDIR /app/CSharpRESTDemo
RUN chmod +x ./start.sh

CMD /bin/bash ./start.sh