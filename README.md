# ESG Intelligent - Energy Monitoring API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-336791?logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-Container-2496ED?logo=docker)
![GitHub Actions](https://img.shields.io/badge/GitHub_Actions-CI/CD-2088FF?logo=githubactions)

REST API for monitoring and managing energy consumption with ESG (Environmental, Social, and Governance) metrics. Built with .NET 8 and Entity Framework Core, PostgreSQL as the database, containerized with Docker and a full CI/CD pipeline via GitHub Actions.

---

## Key Features

- **Energy Monitoring** — Record and query energy consumption data
- **ESG Metrics** — CO₂ reduction and energy efficiency calculations
- **Containerized** — Full execution via Docker Compose
- **Automated CI/CD** — Pipeline with GitHub Actions
- **RESTful API** — Endpoints documented with Swagger
- **Unit Tests** — Coverage with xUnit

---

## Technologies

### Backend
- .NET 8.0
- Entity Framework Core 8.0
- PostgreSQL 15
- xUnit (Tests)
- Swagger / OpenAPI

### DevOps & Infrastructure
- Docker & Docker Compose
- GitHub Actions
- GitHub Container Registry

---

## Prerequisites

- Docker Desktop
- Docker Compose
- Git

---

## Quick Start with Docker

### 1. Clone the repository
```bash
git clone <repository-url>
cd ESGInteligentes
```

### 2. Start the full environment
```bash
docker-compose up -d
```

### 3. Check running services
```bash
docker-compose ps
```

### Test the endpoint
```
http://localhost:8080/api/energia
```

---

## Project Structure

```
ESGInteligentes/
├── .github/workflows/
│   └── ci-cd.yml                 # CI/CD pipeline
├── Controllers/
│   └── EnergiaController.cs      # API controller
├── Data/
│   └── AppDbContext.cs           # Entity Framework context
├── Models/
│   └── ConsumoEnergia.cs         # Data model
├── Tests/
│   └── EnergiaControllerTests.cs # Unit tests
├── Dockerfile                    # Container configuration
├── docker-compose.yml            # Service orchestration
├── Program.cs                    # Entry point
├── appsettings.json              # App settings
└── ESGInteligentes.csproj        # .NET project file
```

---

## API Endpoints

### GET /api/energia
```json
{
  "empresa": "ESG Inteligentes",
  "consumoMensal_kWh": 1250.5,
  "reducaoCO2_ton": 3.2,
  "eficienciaEnergetica": "95%"
}
```

### POST /api/energia
```json
{
  "consumoKWh": 1500.0,
  "dataRegistro": "2024-01-14T10:00:00"
}
```

---

## Dockerfile

Multi-stage build for an optimized image:

```dockerfile
# ---- STAGE 1: build ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ARG PROJECT_FILE=ESGInteligentes.csproj
COPY ["${PROJECT_FILE}", "./"]
RUN dotnet restore "${PROJECT_FILE}"
COPY . .
RUN dotnet publish "${PROJECT_FILE}" -c Release -o /app/publish

# ---- STAGE 2: runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "ESGInteligentes.dll"]
```

---

## Author

Made with 💜 by [VryDeveloper](https://github.com/VryDeveloper)

---

> LEARNING PROJECT
