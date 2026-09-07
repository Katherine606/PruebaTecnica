namespace PruebaTecnicaMVC.Models.DTOs.Vehiculo
{
 
    public class CategoriaDesgloseDto
    {
        public string Categoria { get; set; } = string.Empty;
        public int TotalVehiculos { get; set; }
        public double PromedioKilometraje { get; set; }
        public decimal CostoPromedioAlquiler { get; set; }
        public decimal ValorTotalProyectadoDiario { get; set; }
        public decimal TotalRecargosSeguroLujo { get; set; }
    }
}