FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["School.API/School.API.csproj", "School.API/"]
COPY ["School.Application/School.Application.csproj", "School.Application/"]
COPY ["School.Domain/School.Domain.csproj", "School.Domain/"]
COPY ["SharedKernel.Dto/SharedKernel.Dto.csproj", "SharedKernel.Dto/"]
COPY ["SharedKernel.Enums/SharedKernel.Enums.csproj", "SharedKernel.Enums/"]
COPY ["SharedKernel.Models/SharedKernel.Models.csproj", "SharedKernel.Models/"]
COPY ["SharedKernel.ValueObjects/SharedKernel.ValueObjects.csproj", "SharedKernel.ValueObjects/"]
COPY ["SharedKernel.Interfaces/SharedKernel.Interfaces.csproj", "SharedKernel.Interfaces/"]
COPY ["School.Infrastructure/School.Infrastructure.csproj", "School.Infrastructure/"]
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
