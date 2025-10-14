using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ESGInteligentes.Data;

var builder = WebApplication.CreateBuilder(args);

// DEBUG: Verificar configurações
Console.WriteLine("🔧 Carregando configurações...");
Console.WriteLine($"🔧 Ambiente: {builder.Environment.EnvironmentName}");

// Verificar todas as connection strings disponíveis
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"🔧 Connection String: {connectionString}");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("❌ Connection string não encontrada! Usando fallback...");
    connectionString = "Host=localhost;Port=5432;Database=esgdb;Username=postgres;Password=password123";
}

// Adiciona controladores e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do Entity Framework com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Configuração do ambiente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine("✅ Aplicação iniciada com sucesso!");
app.Run();