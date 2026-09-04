namespace MVCTecnica.Models.DTOs.Vehiculo
{
    public class VehiculoRespuestaDto
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int AnioFabricacion { get; set; }
        public decimal PrecioAlquilerPorDia { get; set; }
        public int Kilometraje { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string EstadoLogico { get; set; } = string.Empty;
        public string ClasificacionKilometraje { get; set; } = string.Empty;
        public decimal RecargoSeguroLujo { get; set; }
      
    }
}