using System.ComponentModel.DataAnnotations;

namespace VehiculoMVC.Models.DTOs.Vehiculo
{
    public class VehiculoCreateDto
    {
        [Required(ErrorMessage = "La placa es obligatoria")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "La placa debe tener entre 5 y 20 caracteres.")]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "La placa solo puede contener letras mayúsculas, números y guiones.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es obligatoria")]
        [StringLength(50, ErrorMessage = "La marca no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s]+$", ErrorMessage = "La marca contiene caracteres no válidos.")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [StringLength(50, ErrorMessage = "El modelo no puede superar los 50 caracteres.")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(1900, 2100, ErrorMessage = "El año de fabricación no es válido.")]
        public int AnioFabricacion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio de alquiler por día debe ser mayor a 0.")]
        public decimal PrecioAlquilerPorDia { get; set; }

        [Required(ErrorMessage = "El kilometraje es obligatorio")]
        [Range(0, 1000000, ErrorMessage = "El kilometraje no puede ser negativo ni exceder los límites lógicos.")]
        public int Kilometraje { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
 
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado operativo es obligatorio.")]
        [RegularExpression("Disponible|Alquilado|En Mantenimiento", ErrorMessage = "El estado debe ser: Disponible, Alquilado o En Mantenimiento.")]
        public string Estado { get; set; } = "Disponible";

        [RegularExpression("A|I|N", ErrorMessage = "El estado lógico debe ser A (Activo), I (Inactivo) o N (Eliminado).")]
        public string EstadoLogico { get; set; } = "A";
    }
}