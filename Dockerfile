FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 4022
EXPOSE 4023

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY MasterData.csproj MasterData/MasterData.csproj
Copy Protos/Area.proto MasterData/Protos/Area.proto
Copy Protos/Company.proto MasterData/Protos/Company.proto
Copy Protos/Pool.proto MasterData/Protos/Pool.proto
Copy Protos/ServiceType.proto MasterData/Protos/ServiceType.proto
Copy google/api/annotations.proto MasterData/google/api/annotations.proto
COPY google/api/http.proto MasterData/google/api/http.proto
COPY . .
RUN dotnet restore "MasterData/MasterData.csproj" 

WORKDIR /src/MasterData
COPY . .
RUN rm -rf Common

RUN dotnet build "MasterData.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterData.csproj" -c Release -o /app/publish  --no-restore
RUN dotnet dev-certs https

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:4022;https://*:4023
ENV DOTNET_EnableDiagnostics=0
ENTRYPOINT ["dotnet", "MasterData.dll"]
