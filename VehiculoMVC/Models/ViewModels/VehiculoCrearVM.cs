using System.ComponentModel.DataAnnotations;

namespace VehiculoMVC.Models.ViewModels
{
    public class VehiculoCrearVM
    {
        [Required(ErrorMessage = "La placa es obligatoria.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "La placa debe tener entre 5 y 20 caracteres.")]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "La placa solo admite letras mayúsculas, números y guiones.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [StringLength(50, ErrorMessage = "La marca no puede superar los 50 caracteres.")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El modelo no puede superar los 50 caracteres.")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año de fabricación es obligatorio.")]
        [Range(1900, 2100, ErrorMessage = "Ingrese un año válido entre 1900 y 2100.")]
        public int AnioFabricacion { get; set; } = DateTime.Now.Year; //solo el añito

        [Required(ErrorMessage = "El precio de alquiler por día es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal PrecioAlquilerPorDia { get; set; }

        [Required(ErrorMessage = "El kilometraje es obligatorio.")]
        [Range(0, 1000000, ErrorMessage = "El kilometraje debe estar entre 0 y 1,000,000.")]
        public int Kilometraje { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [RegularExpression(@"^(Sedán|SUV|Camioneta|Lujo)$", ErrorMessage = "Seleccione una categoría válida.")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado operativo es obligatorio.")]
        [RegularExpression(@"^(Disponible|Alquilado|En Mantenimiento)$", ErrorMessage = "Seleccione un estado válido.")]
        public string Estado { get; set; } = "Disponible";

        public string EstadoLogico { get; set; } = "A";
    }

}