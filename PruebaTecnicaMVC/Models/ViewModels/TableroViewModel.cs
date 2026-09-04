namespace PruebaTecnicaMVC.Models.ViewModels
{

    public class TableroViewModel
    {
        public int TotalVehiculos { get; set; }
        public decimal ValorMonetarioTotalProyectadoDiario { get; set; }
        public List<CategoriaDesgloseViewModel> DesglosePorCategoria { get; set; } = new();
        public List<VehiculoViewModel> VehiculosRequierenAtencion { get; set; } = new();
        public List<VehiculoViewModel> DetalleFlota { get; set; } = new();
    }

}
