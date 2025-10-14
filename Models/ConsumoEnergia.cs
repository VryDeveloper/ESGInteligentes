namespace ESGInteligentes.Models
{
    public class ConsumoEnergia
    {
        public int Id { get; set; }
        public double ConsumoKWh { get; set; }
        public DateTime DataRegistro { get; set; }
        public string Empresa { get; set; } = "ESG Inteligentes";
        public double ReducaoCO2Ton { get; set; }
        public string EficienciaEnergetica { get; set; } = "95%";
    }
}