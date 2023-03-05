#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN mkdir /var/db

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["tasks/tasks.csproj", "tasks/"]
RUN dotnet restore "tasks/tasks.csproj"
COPY . .
WORKDIR "/src/tasks"
RUN dotnet build "tasks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tasks.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tasks.dll"]