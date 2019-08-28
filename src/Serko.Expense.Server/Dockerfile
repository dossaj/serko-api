FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Serko.Expense.Server/Serko.Expense.Server.csproj", "Serko.Expense.Server/"]
RUN dotnet restore "src/Serko.Expense.Server/Serko.Expense.Server.csproj"
COPY . .
WORKDIR /Serko.Expense.Server
RUN dotnet build "Serko.Expense.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Serko.Expense.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Serko.Expense.Server.dll"]
