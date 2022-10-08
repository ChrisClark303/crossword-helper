#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5432:5432

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CrosswordHelper.Api/CrosswordHelper.Api.csproj", "CrosswordHelper.Api/"]
COPY ["CrosswordHelper.Data/CrosswordHelper.Data.csproj", "CrosswordHelper.Data/"]
COPY ["CrosswordHelper.Data.Postgres/CrosswordHelper.Data.Postgres.csproj", "CrosswordHelper.Data.Postgres/"]
RUN dotnet restore "CrosswordHelper.Api/CrosswordHelper.Api.csproj"
COPY . .
WORKDIR "/src/CrosswordHelper.Api"
RUN dotnet build "CrosswordHelper.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrosswordHelper.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrosswordHelper.Api.dll"]