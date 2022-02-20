FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TeamManagementAPI/TeamManagementAPI.csproj","src/TeamManagementAPI/"]
COPY ["src/Core/Application/Application.csproj","src/Application/"]
COPY ["src/Core/Domain/Domain.csproj","src/Domain/"]
COPY ["src/TeamManagement.Services/TeamManagement.Services.csproj","src/TeamManagement.Services/"]
COPY ["src/Infrastructure/Infrastructure.csproj","src/Infrastructure/"]
COPY ["src/Shared.DTO/Shared.DTO.csproj","src/Shared.DTO/"]
RUN dotnet restore "src/TeamManagementAPI/TeamManagementAPI.csproj" --disable-parallel
COPY . .
WORKDIR "/src/TeamManagementAPI"
RUN dotnet build "TeamManagementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamManagementAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamManagement.TeamManagementAPI.dll"]

