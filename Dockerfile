FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./KasetMore/KasetMore.csproj"
RUN dotnet publish "./KasetMore/KasetMore.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080

ENTRYPOINT ["dotnet", "KasetMore.dll"]