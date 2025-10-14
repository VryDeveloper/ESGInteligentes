using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESGInteligentes.Data;
using ESGInteligentes.Models;

namespace ESGInteligentes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnergiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnergiaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumo()
        {
            var ultimoRegistro = await _context.ConsumosEnergia
                .OrderByDescending(c => c.DataRegistro)
                .FirstOrDefaultAsync();

            if (ultimoRegistro == null)
            {
                // Retorna dados padrão se não houver registros
                return Ok(new
                {
                    empresa = "ESG Inteligentes",
                    consumoMensal_kWh = 1250.5,
                    reducaoCO2_ton = 3.2,
                    eficienciaEnergetica = "95%"
                });
            }

            return Ok(new
            {
                empresa = ultimoRegistro.Empresa,
                consumoMensal_kWh = ultimoRegistro.ConsumoKWh,
                reducaoCO2_ton = ultimoRegistro.ReducaoCO2Ton,
                eficienciaEnergetica = ultimoRegistro.EficienciaEnergetica
            });
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarConsumo([FromBody] ConsumoModel consumo)
        {
            if (consumo == null || consumo.ConsumoKWh <= 0)
                return BadRequest("Dados inválidos.");

            var registro = new ConsumoEnergia
            {
                ConsumoKWh = consumo.ConsumoKWh,
                DataRegistro = consumo.DataRegistro,
                ReducaoCO2Ton = consumo.ConsumoKWh * 0.0005 // Exemplo de cálculo
            };

            _context.ConsumosEnergia.Add(registro);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Consumo registrado com sucesso!",
                consumo.ConsumoKWh,
                consumo.DataRegistro,
                id = registro.Id
            });
        }
    }

    public class ConsumoModel
    {
        public double ConsumoKWh { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}