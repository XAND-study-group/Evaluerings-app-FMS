FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["School.API/School.API.csproj", "School.API/"]
COPY ["Module.Shared/Module.Shared.csproj", "Module.Shared/"]
COPY ["Module.User/Module.User.csproj", "Module.User/"]
COPY ["Module.User.Application/Module.User.Application.csproj", "Module.User.Application/"]
COPY ["Module.User.Domain/Module.User.Domain.csproj", "Module.User.Domain/"]
COPY ["SharedKernel.Enums/SharedKernel.Enums.csproj", "SharedKernel.Enums/"]
COPY ["SharedKernel.ValueObjects/SharedKernel.ValueObjects.csproj", "SharedKernel.ValueObjects/"]
COPY ["SharedKernel.Dto/SharedKernel.Dto.csproj", "SharedKernel.Dto/"]
COPY ["Module.User.Infrastructure/Module.User.Infrastructure.csproj", "Module.User.Infrastructure/"]
COPY ["Module.Shared.Infrastructure/Module.Shared.Infrastructure.csproj", "Module.Shared.Infrastructure/"]
RUN dotnet restore "School.API/School.API.csproj"
COPY . .
WORKDIR "/src/School.API"
RUN dotnet build "School.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "School.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "School.API.dll"]
