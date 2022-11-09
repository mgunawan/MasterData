#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 4022
EXPOSE 4423

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MasterData/MasterData.csproj", "MasterData/"]
COPY ["../Common/Common.csproj", "Common/"]
RUN dotnet restore "MasterData/MasterData.csproj"
COPY . .
WORKDIR "/src/MasterData"
RUN dotnet build "MasterData.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterData.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet dev-certs https

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:4022;https://*:4023
ENTRYPOINT ["dotnet", "MasterData.dll"]