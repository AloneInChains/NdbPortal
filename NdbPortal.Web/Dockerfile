#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NdbPortal.Web/NdbPortal.Web.csproj", "NdbPortal.Web/"]
RUN dotnet restore "NdbPortal.Web/NdbPortal.Web.csproj"
COPY . .
WORKDIR "/src/NdbPortal.Web"
RUN dotnet build "NdbPortal.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NdbPortal.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NdbPortal.Web.dll"]