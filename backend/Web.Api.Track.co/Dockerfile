FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

COPY . ./

RUN dotnet restore

# Runs in Release mode: no debug information
# RUN dotnet publish -c Release -o out 

# Runs in Debug mode: with debug information
RUN dotnet publish -c Debug -o out 

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Web.Api.Track.co.dll"]
