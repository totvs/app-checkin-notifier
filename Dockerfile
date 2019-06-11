FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
ADD NuGet.Config .
WORKDIR /src
COPY ["src/App.CheckIn.Notifier.Web/App.CheckIn.Notifier.Web.csproj", "src/App.CheckIn.Notifier.Web/"]
COPY ["src/App.CheckIn.Notifier.Application/App.CheckIn.Notifier.Application.csproj", "src/App.CheckIn.Notifier.Application/"]
COPY ["src/App.CheckIn.EntityFrameworkCore/App.CheckIn.EntityFrameworkCore.csproj", "src/App.CheckIn.EntityFrameworkCore/"]
COPY ["src/App.CheckIn.Domain/App.CheckIn.Domain.csproj", "src/App.CheckIn.Domain/"]
RUN dotnet restore "src/App.CheckIn.Notifier.Web/App.CheckIn.Notifier.Web.csproj"
COPY . .
WORKDIR "/src/src/App.CheckIn.Notifier.Web"
RUN dotnet build "App.CheckIn.Notifier.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "App.CheckIn.Notifier.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App.CheckIn.Notifier.Web.dll"]
