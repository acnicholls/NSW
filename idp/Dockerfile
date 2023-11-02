#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["idp/src/IDP.csproj", "idp/src/"]
RUN dotnet restore "idp/src/IDP.csproj"
COPY . .
WORKDIR "/src/idp/src"
RUN dotnet build "IDP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IDP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5006
EXPOSE 5007

ENTRYPOINT ["dotnet", "IDP.dll"]