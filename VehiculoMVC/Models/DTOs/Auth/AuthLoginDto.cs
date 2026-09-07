using System.ComponentModel.DataAnnotations;

namespace VehiculoMVC.Models.DTOs.Auth
{
    public class AuthLoginDto
    {
        [Required(ErrorMessage = "El username es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$",
       ErrorMessage = "El username solo puede contener letras, números y guion bajo.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$",
        ErrorMessage = "La contraseña debe contener al menos una letra y un número.")]
        public string Password { get; set; } = null!;

    }
}
