using Microsoft.AspNetCore.Mvc;
using ESGInteligentes.Controllers;
using ESGInteligentes.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ESGInteligentes.Tests;

public class EnergiaControllerTests
{
    private readonly EnergiaController _controller;

    public EnergiaControllerTests()
    {
        // Configura um DbContext em memória para testes
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new AppDbContext(options);
        _controller = new EnergiaController(context);
    }

    [Fact]
    public async Task GetConsumo_DeveRetornarOkResult()
    {
        // Act
        var result = await _controller.GetConsumo();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task RegistrarConsumo_ComDadosValidos_DeveRetornarOk()
    {
        // Arrange
        var consumo = new ConsumoModel
        {
            ConsumoKWh = 1000.0,
            DataRegistro = DateTime.Now
        };

        // Act
        var result = await _controller.RegistrarConsumo(consumo);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task RegistrarConsumo_ComConsumoZero_DeveRetornarBadRequest()
    {
        // Arrange
        var consumo = new ConsumoModel
        {
            ConsumoKWh = 0,
            DataRegistro = DateTime.Now
        };

        // Act
        var result = await _controller.RegistrarConsumo(consumo);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegistrarConsumo_ComConsumoNegativo_DeveRetornarBadRequest()
    {
        // Arrange
        var consumo = new ConsumoModel
        {
            ConsumoKWh = -100,
            DataRegistro = DateTime.Now
        };

        // Act
        var result = await _controller.RegistrarConsumo(consumo);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegistrarConsumo_ComModelNulo_DeveRetornarBadRequest()
    {
        // Arrange
        ConsumoModel consumo = null!;

        // Act
        var result = await _controller.RegistrarConsumo(consumo);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
