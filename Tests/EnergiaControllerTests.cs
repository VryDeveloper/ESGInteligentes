using Microsoft.AspNetCore.Mvc;
using ESGInteligentes.Controllers;
using Xunit;

namespace ESGInteligentes.Tests;

public class EnergiaControllerTests
{
    private readonly EnergiaController _controller;

    public EnergiaControllerTests()
    {
        _controller = new EnergiaController();
    }

    [Fact]
    public void GetConsumo_DeveRetornarOkResult()
    {
        // Act
        var result = _controller.GetConsumo();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void GetConsumo_DeveRetornarDadosCorretos()
    {
        // Act
        var result = _controller.GetConsumo() as OkObjectResult;
        var dados = result?.Value;

        // Assert
        Assert.NotNull(dados);

        // Usando dynamic para acessar as propriedades sem reflection complexa
        dynamic dadosDinamicos = dados!;
        Assert.Equal("ESG Inteligentes", dadosDinamicos.empresa);
        Assert.Equal(1250.5, dadosDinamicos.consumoMensal_kWh);
        Assert.Equal(3.2, dadosDinamicos.reducaoCO2_ton);
        Assert.Equal("95%", dadosDinamicos.eficienciaEnergetica);
    }

    [Fact]
    public void RegistrarConsumo_ComDadosValidos_DeveRetornarOk()
    {
        // Arrange
        var consumo = new ConsumoModel
        {
            ConsumoKWh = 1000.0,
            DataRegistro = DateTime.Now
        };

        // Act
        var result = _controller.RegistrarConsumo(consumo);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public void RegistrarConsumo_ComConsumoZero_DeveRetornarBadRequest()
    {
        // Arrange
        var consumo = new ConsumoModel
        {
            ConsumoKWh = 0,
            DataRegistro = DateTime.Now
        };

        // Act
        var result = _controller.RegistrarConsumo(consumo);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Dados inválidos.", badRequestResult.Value);
    }

    [Fact]
    public void RegistrarConsumo_ComConsumoNegativo_DeveRetornarBadRequest()
    {
        // Arrange
        var consumo = new ConsumoModel
        {
            ConsumoKWh = -100,
            DataRegistro = DateTime.Now
        };

        // Act
        var result = _controller.RegistrarConsumo(consumo);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void RegistrarConsumo_ComModelNulo_DeveRetornarBadRequest()
    {
        // Arrange
        ConsumoModel consumo = null!;

        // Act
        var result = _controller.RegistrarConsumo(consumo);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Dados inválidos.", badRequestResult.Value);
    }

    [Fact]
    public void RegistrarConsumo_DeveRetornarMensagemSucesso()
    {
        // Arrange
        var dataTeste = new DateTime(2024, 1, 15);
        var consumo = new ConsumoModel
        {
            ConsumoKWh = 1500.0,
            DataRegistro = dataTeste
        };

        // Act
        var result = _controller.RegistrarConsumo(consumo) as OkObjectResult;
        var resultado = result?.Value;

        // Assert
        Assert.NotNull(resultado);

        dynamic resultadoDinamico = resultado!;
        Assert.Equal("Consumo registrado com sucesso!", resultadoDinamico.mensagem);
        Assert.Equal(1500.0, resultadoDinamico.ConsumoKWh);
        Assert.Equal(dataTeste, resultadoDinamico.DataRegistro);
    }
}