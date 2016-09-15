FROM microsoft/dotnet:1.0.0-core
WORKDIR /app
ENTRYPOINT ["dotnet", "angularmovie-aspnetcore.dll"]
COPY . /app
