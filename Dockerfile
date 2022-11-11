FROM mcr.microsoft.com/dotnet/sdk:6.0.302 as builder
WORKDIR /app
COPY MasterData.csproj .
COPY ../Common/Common.csproj .
RUN dotnet restore MasterData.csproj -r linux-musl-x64
COPY . .
RUN dotnet publish MasterData.csproj -p:PublishSingleFile=true -r linux-musl-x64 --self-contained true -p:PublishTrimmed=True -p:TrimMode=Link -c release -o /MasterData --no-restore

WORKDIR /app
COPY --from=builder /MasterData .
ENV ASPNETCORE_URLS=http://*:4022;https://*:4023
ENV DOTNET_EnableDiagnostics=0
ENTRYPOINT ["/app/MasterData"]