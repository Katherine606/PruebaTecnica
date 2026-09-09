namespace VehiculoMVC.Models.ViewModels
{
    public class VehiculoAtencionViewModel
    {
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int Kilometraje { get; set; }
        public string ClasificacionKilometraje { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}