#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Taste/Taste.csproj", "Taste/"]
COPY ["Taste.DataAccess/Taste.DataAccess.csproj", "Taste.DataAccess/"]
COPY ["Taste.Models/Taste.Models.csproj", "Taste.Models/"]
COPY ["Taste.Utility/Taste.Utility.csproj", "Taste.Utility/"]
RUN dotnet restore "Taste/Taste.csproj"
COPY . .
WORKDIR "/src/Taste"
RUN dotnet build "Taste.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Taste.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taste.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Taste.dll