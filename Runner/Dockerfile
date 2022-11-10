FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Runner.csproj", "Runner/"]
RUN dotnet restore "Runner/Runner.csproj"

COPY . Runner/.
RUN dotnet publish -c Release -o /app Runner/Runner.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Runner.dll"]