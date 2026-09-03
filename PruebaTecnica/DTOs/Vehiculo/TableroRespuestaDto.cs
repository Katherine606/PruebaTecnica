namespace PruebaTecnica.DTOs.Vehiculo
{
    public class TableroRespuestaDto
    {
            public int TotalVehiculos { get; set; }
            public decimal ValorMonetarioTotalProyectadoDiario { get; set; }
            public IEnumerable<CategoriaDesgloseDto> DesglosePorCategoria { get; set; } = new List<CategoriaDesgloseDto>();
            public IEnumerable<VehiculoRespuestaDto> VehiculosRequierenAtencion { get; set; } = new List<VehiculoRespuestaDto>();
            public IEnumerable<VehiculoRespuestaDto> DetalleFlota { get; set; } = new List<VehiculoRespuestaDto>();
    
    }
}
