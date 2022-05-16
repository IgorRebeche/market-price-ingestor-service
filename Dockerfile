FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out
RUN rm ./nuget.config

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /WebApi
COPY --from=build-env /app/out .
ENV ASPNETCORE_ENVIRONMENT Docker
ENV ASPNETCORE_URLS http://0.0.0.0:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "WebApi.dll"]