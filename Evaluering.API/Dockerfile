FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Evaluering.API/Evaluering.API.csproj", "Evaluering.API/"]
COPY ["Module.Feedback.Domain/Module.Feedback.Domain.csproj", "Module.Feedback.Domain/"]
COPY ["SharedKernel.Dto/SharedKernel.Dto.csproj", "SharedKernel.Dto/"]
COPY ["SharedKernel.Enums/SharedKernel.Enums.csproj", "SharedKernel.Enums/"]
COPY ["SharedKernel.ValueObjects/SharedKernel.ValueObjects.csproj", "SharedKernel.ValueObjects/"]
COPY ["SharedKernel.Interfaces/SharedKernel.Interfaces.csproj", "SharedKernel.Interfaces/"]
COPY ["Module.Feedback/Module.Feedback.csproj", "Module.Feedback/"]
COPY ["Module.Feedback.Application/Module.Feedback.Application.csproj", "Module.Feedback.Application/"]
COPY ["SharedKernel.Models/SharedKernel.Models.csproj", "SharedKernel.Models/"]
COPY ["Module.Feedback.Infrastructure/Module.Feedback.Infrastructure.csproj", "Module.Feedback.Infrastructure/"]
COPY ["Module.ExitSlips.Infrastructure/Module.ExitSlip.Infrastructure.csproj", "Module.ExitSlips.Infrastructure/"]
COPY ["Module.ExitSlips.Application/Module.ExitSlip.Application.csproj", "Module.ExitSlips.Application/"]
COPY ["Module.ExitSlips.Domain/Module.ExitSlip.Domain.csproj", "Module.ExitSlips.Domain/"]
RUN dotnet restore "Evaluering.API/Evaluering.API.csproj"
COPY . .
WORKDIR "/src/Evaluering.API"
RUN dotnet build "Evaluering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Evaluering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evaluering.API.dll"]
