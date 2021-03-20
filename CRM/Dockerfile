#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CRM.csproj", "./"]
RUN dotnet restore "CRM.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "CRM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRM.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get update && apt-get install -y net-tools curl iputils-ping telnetd telnet nano vim libc6-dev libgdiplus dnsutils

WORKDIR /app
EXPOSE 8080
USER root
RUN chmod -R g+w /app

COPY --from=publish /app/publish .

COPY ["docker-entrypoint.sh", "."]
RUN chmod a+x docker-entrypoint.sh
CMD ["./docker-entrypoint.sh"]
