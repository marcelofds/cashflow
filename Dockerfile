FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CashFlow.WebApi/CashFlow.WebApi.csproj", "src/CashFlow.WebApi/"]
COPY ["src/CashFlow.Domain/CashFlow.Domain.csproj", "src/CashFlow.Domain/"]
COPY ["src/CashFlow.Data/CashFlow.Data.csproj", "src/CashFlow.Data/"]
COPY ["src/CashFlow.Application/CashFlow.Application.csproj", "src/CashFlow.Application/"]
COPY ["src/CashFlow.IoC/CashFlow.IoC.csproj", "src/CashFlow.IoC/"]
RUN dotnet restore "src/CashFlow.WebApi/CashFlow.WebApi.csproj"
COPY . .
WORKDIR "/src/src/CashFlow.WebApi"
RUN dotnet build "CashFlow.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashFlow.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashFlow.WebApi.dll"]
