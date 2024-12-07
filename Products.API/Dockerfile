FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build-env
WORKDIR /App

COPY ./MetaCortex.Products.sln ./

COPY ./Products.API/MetaCortex.Products.API.csproj ./Products.API/
COPY ./MetaCortex.Products.DataAccess/MetaCortex.Products.DataAccess.csproj ./MetaCortex.Products.DataAccess/
RUN dotnet restore MetaCortex.Products.sln

COPY ./ ./

RUN dotnet publish Products.API/MetaCortex.Products.API.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff
WORKDIR /App
COPY --from=build-env /App/out .

# Expose port and define entry point
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "MetaCortex.Products.API.dll"]