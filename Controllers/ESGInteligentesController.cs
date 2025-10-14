using Microsoft.AspNetCore.Mvc;

namespace ESGInteligentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnergiaController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetConsumo()
        {
            var dados = new
            {
                empresa = "ESG Inteligentes",
                consumoMensal_kWh = 1250.5,
                reducaoCO2_ton = 3.2,
                eficienciaEnergetica = "95%"
            };

            return Ok(dados);
        }

        [HttpPost]
        public IActionResult RegistrarConsumo([FromBody] ConsumoModel consumo)
        {
            if (consumo == null || consumo.ConsumoKWh <= 0)
                return BadRequest("Dados inválidos.");

            var resultado = new
            {
                mensagem = "Consumo registrado com sucesso!",
                consumo.ConsumoKWh,
                consumo.DataRegistro
            };

            return Ok(resultado);
        }
    }

    public class ConsumoModel
    {
        public double ConsumoKWh { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
