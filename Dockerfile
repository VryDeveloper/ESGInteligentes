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
