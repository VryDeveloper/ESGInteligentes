# ESG Inteligentes - API de Monitoramento Energético

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-336791?logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-Container-2496ED?logo=docker)
![GitHub Actions](https://img.shields.io/badge/GitHub_Actions-CI/CD-2088FF?logo=githubactions)

API REST para monitoramento e gestão de consumo energético com métricas ESG (Environmental, Social, and Governance). Desenvolvida em .NET 8 com Entity Framework e PostgreSQL, containerizada com Docker e com pipeline completo de CI/CD.

##  Características Principais

- **Monitoramento Energético**: Registro e consulta de consumo energético
- **Métricas ESG**: Cálculo de redução de CO₂ e eficiência energética
- **Containerizada**: Execução completa via Docker Compose
- **CI/CD Automatizado**: Pipeline com GitHub Actions
- **API RESTful**: Endpoints documentados com Swagger
- **Testes Unitários**: Cobertura com xUnit

##  Tecnologias Utilizadas

### Backend
- .NET 8.0
- Entity Framework Core 8.0
- PostgreSQL 15
- xUnit (Testes)
- Swagger/OpenAPI

### DevOps & Infraestrutura
- Docker & Docker Compose
- GitHub Actions
- GitHub Container Registry

##  Pré-requisitos

- Docker Desktop
- Docker Compose
- Git

##  Execução Rápida com Docker

### 1. Clone o repositório
```
git clone <url-do-repositorio>
cd ESGInteligentes
```
### 2. Execute o ambiente completo
```
docker-compose up -d
```
### 3. Verifique os serviços
```
docker-compose ps
```
### Consultar consumo energético (Testar endpoint)
http://localhost:8080/api/energia
```
ESGInteligentes/
├── .github/workflows/
│   └── ci-cd.yml                 # Pipeline de CI/CD
├── Controllers/
│   └── EnergiaController.cs      # Controlador da API
├── Data/
│   └── AppDbContext.cs           # Contexto do Entity Framework
├── Models/
│   └── ConsumoEnergia.cs         # Modelo de dados
├── Tests/
│   └── EnergiaControllerTests.cs # Testes unitários
├── Dockerfile                    # Configuração do container
├── docker-compose.yml            # Orquestração de serviços
├── Program.cs                    # Ponto de entrada
├── appsettings.json              # Configurações
└── ESGInteligentes.csproj        # Projeto .NET
```
### GET /api/energia
```
{
  "empresa": "ESG Inteligentes",
  "consumoMensal_kWh": 1250.5,
  "reducaoCO2_ton": 3.2,
  "eficienciaEnergetica": "95%"
}
```
### POST /api/energia
```
{
  "consumoKWh": 1500.0,
  "dataRegistro": "2024-01-14T10:00:00"
}
```
# Dockerfile

### Multi-stage build para imagem otimizada
```
# ---- STAGE 1: build ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ARG PROJECT_FILE=ESGInteligentes.csproj
COPY ["${PROJECT_FILE}", "./"]

RUN dotnet restore "${PROJECT_FILE}"

# copia todo o código e publica
COPY . .
RUN dotnet publish "${PROJECT_FILE}" -c Release -o /app/publish

# ---- STAGE 2: runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
# expõe porta
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ESGInteligentes.dll"]

```
