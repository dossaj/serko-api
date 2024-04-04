FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Serko.Expense.Server/Serko.Expense.Server.csproj", "src/Serko.Expense.Server/"]
COPY ["src/Serko.Expense.Castle/Serko.Expense.Castle.csproj", "src/Serko.Expense.Castle/"]
COPY ["src/Serko.Expense.Core/Serko.Expense.Core.csproj", "src/Serko.Expense.Core/"]
COPY ["src/Serko.Expense.Domain/Serko.Expense.Domain.csproj", "src/Serko.Expense.Domain/"]
RUN dotnet restore "src/Serko.Expense.Server/Serko.Expense.Server.csproj"
COPY . .
WORKDIR "/src/src/Serko.Expense.Server"
RUN dotnet build "Serko.Expense.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Serko.Expense.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Serko.Expense.Server.dll"]