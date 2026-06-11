# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo do projeto
COPY ["ProjetoEjaCast.csproj", "./"]

# Restaura dependências
RUN dotnet restore

# Copia o restante dos arquivos
COPY . .

# Publica a aplicação
RUN dotnet publish -c Release -o /app/publish

# Etapa de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ProjetoEjaCast.dll"]